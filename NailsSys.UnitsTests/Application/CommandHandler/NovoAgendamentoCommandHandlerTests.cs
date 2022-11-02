using System.Linq.Expressions;
using Bogus;
using Moq;
using NailsSys.Application.Commands.AgendamentoCommands.NovoAgendamento;
using NailsSys.Application.InputModels;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class NovoAgendamentoCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private readonly Mock<IAgendamentoRepository> _agendamentoRepository;
        private readonly Mock<IItemAgendamentoRepository> _itemAgendamentoRepository;
        private readonly Mock<IClienteRepository> _clienteRepository;
        private readonly NovoAgendamentoCommandHandler _novoAgendamentoCommandHandler;
        private readonly NovoAgendamentoCommand _novoAgendamentoCommand;
        private readonly Cliente _cliente;
        private readonly NovoAgendamentoCommandValidation _novoAgendamentoCommandValidation;

        public NovoAgendamentoCommandHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
            _agendamentoRepository = new Mock<IAgendamentoRepository>();
            _itemAgendamentoRepository = new Mock<IItemAgendamentoRepository>();
            _clienteRepository = new Mock<IClienteRepository>();

            _unitOfWorks.SetupGet(x => x.Agendamento).Returns(_agendamentoRepository.Object);
            _unitOfWorks.SetupGet(x => x.ItemAgendamento).Returns(_itemAgendamentoRepository.Object);
            _unitOfWorks.SetupGet(x => x.Cliente).Returns(_clienteRepository.Object);

            _novoAgendamentoCommandHandler = new NovoAgendamentoCommandHandler(_unitOfWorks.Object);
            _novoAgendamentoCommand = new Faker<NovoAgendamentoCommand>()
                .RuleFor(x => x.IdCliente, y=>y.Random.Int(0,10))
                .RuleFor(x => x.DataAtendimento, y=>y.Date.Recent())
                .RuleFor(x => x.InicioPrevisto, y=>y.Date.Recent())
                .RuleFor(x => x.TerminoPrevisto, y=>y.Date.Recent(2))
                .Generate();

            _cliente = new Cliente(new Faker().Person.FullName,new Faker().Phone.PhoneNumber());

            _novoAgendamentoCommandValidation = new NovoAgendamentoCommandValidation();
        }

        [Fact]
        public async Task NovoAgendamentoCommandValido_QuandoExecutado_CriarAgendamentoAsync()
        {
            //Arrange
            _novoAgendamentoCommand.Itens = new List<ItemInputModel>(
                new Faker<ItemInputModel>()
                    .RuleFor(x=> x.IdProduto,y=> y.Random.Int(0,50))
                    .RuleFor(x=> x.Quantidade,y=> y.Random.Int(0,50))
                    .Generate(5)
            );
            _clienteRepository.Setup(x=>x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(_cliente);
            _agendamentoRepository.Setup(x=> x.MaxAsync(It.IsAny<Expression<Func<Agendamento,int>>>())).ReturnsAsync(10);
            _itemAgendamentoRepository.Setup(x=> x.ObterMaxItem(It.IsAny<int>())).ReturnsAsync(1);

            //Act
            await _novoAgendamentoCommandHandler.Handle(_novoAgendamentoCommand,new CancellationToken());
            //Assert
            _agendamentoRepository.Verify(x=> x.InserirAsync(It.IsAny<Agendamento>()),Times.Once);
            _itemAgendamentoRepository.Verify(x=> x.InserirItemAsync(It.IsAny<ItemAgendamento>()),Times.Exactly(5));
            _unitOfWorks.Verify(x=> x.SaveChangesAsync(),Times.Exactly(6));
            _unitOfWorks.Verify(x=>x.CommitAsync(),Times.Once);
        }

        [Fact]
        public void AgedamentoCommandSemItens_QuandoExecutado_RetornarExcecao()
        {
            //Arrange 
            _clienteRepository.Setup(x=>x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(_cliente);
            //Act - Assert
            var erro = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _novoAgendamentoCommandHandler.Handle(_novoAgendamentoCommand,new CancellationToken()));
            Assert.NotNull(erro.Result);
            Assert.Equal("Nenhum produto informado para realizar o agendamento.",erro.Result.Message);
        }

        [Fact]
        public void AgedamentoCommandClienteInvalido_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var erro = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _novoAgendamentoCommandHandler.Handle(_novoAgendamentoCommand,new CancellationToken()));
            Assert.NotNull(erro.Result);
            Assert.Equal("O cliente informado não existe.",erro.Result.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void IdClienteInvalido_RetornarExcecaoFluentValidation(int idCliente)
        {
            //Arrange
            var novoAgendamentoCommand = new NovoAgendamentoCommand{IdCliente = idCliente};
            //Act
            var result = _novoAgendamentoCommandValidation.Validate(novoAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensAgendamento.ClienteNullVazio));
        }

        [Theory]
        [InlineData("2022-9-15 10:30:40")]
        public void DataAtendimentoInvalido_RetornarExcecaoFluentValidation(DateTime dataAtendimento)
        {
            //Arrange
            var novoAgendamentoCommand = new NovoAgendamentoCommand{DataAtendimento = dataAtendimento};
            //Act
            var result = _novoAgendamentoCommandValidation.Validate(novoAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensAgendamento.DataAtendimentoInvalida) ||
                        erros.Contains(MensagensAgendamento.DataAtendimentoNullVazia));
        }

        [Theory]
        [InlineData("2022-9-10 10:30:15","2022-9-10 10:40:15")]
        [InlineData("2022-9-17 10:30:15","2022-9-17 10:20:15")]
        public void HorariosInvalido_RetornarExcecaoFluentValidation(DateTime inicio,DateTime fim)
        {
            //Arrange
            var novoAgendamentoCommand = new NovoAgendamentoCommand{InicioPrevisto = inicio,TerminoPrevisto = fim};
            //Act
            var result = _novoAgendamentoCommandValidation.Validate(novoAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensAgendamento.HorarioInvalido) ||
                        erros.Contains(MensagensAgendamento.HorarioNullVazio) ||
                        erros.Contains(MensagensAgendamento.TerminoAtendimentoMaiorQueInicioDoAtendimento));
        }
    }
}