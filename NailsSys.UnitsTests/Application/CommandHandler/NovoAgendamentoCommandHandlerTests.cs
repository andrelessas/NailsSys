using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using Moq.AutoMock;
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
        private readonly AutoMocker _mocker;
        private readonly NovoAgendamentoCommandHandler _novoAgendamentoCommandHandler;
        private readonly NovoAgendamentoCommand _novoAgendamentoCommand;
        private readonly NovoAgendamentoCommandValidation _novoAgendamentoCommandValidation;

        public NovoAgendamentoCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _novoAgendamentoCommandHandler = _mocker.CreateInstance<NovoAgendamentoCommandHandler>();
            _novoAgendamentoCommand = new Faker<NovoAgendamentoCommand>()
                .RuleFor(x => x.IdCliente, y=>y.Random.Int(0,10))
                .RuleFor(x => x.DataAtendimento, y=>y.Date.Recent())
                .RuleFor(x => x.InicioPrevisto, y=>y.Date.Recent())
                .RuleFor(x => x.TerminoPrevisto, y=>y.Date.Recent(2))
                .Generate();

            _novoAgendamentoCommandValidation = new NovoAgendamentoCommandValidation();
        }

        [Fact]
        public async Task NovoAgendamentoCommandValido_QuandoExecutado_CriarAgendamentoAsync()
        {
            //Arrange
            _novoAgendamentoCommand.Itens = new List<ItemAgendamentoInputModel>(
                new Faker<ItemAgendamentoInputModel>()
                    .RuleFor(x=> x.IdProduto,y=> y.Random.Int(0,50))
                    .RuleFor(x=> x.Quantidade,y=> y.Random.Int(0,50))
                    .Generate(5)
            );
            _mocker.GetMock<IAgendamentoRepository>().Setup(x=> x.ObterMaxAgendamento()).ReturnsAsync(10);
            _mocker.GetMock<IItemAgendamentoRepository>().Setup(x=> x.ObterMaxItem(It.IsAny<int>())).ReturnsAsync(1);

            //Act
            await _novoAgendamentoCommandHandler.Handle(_novoAgendamentoCommand,new CancellationToken());
            //Assert
            _mocker.GetMock<IAgendamentoRepository>().Verify(x=> x.InserirAsync(It.IsAny<Agendamento>()),Times.Once);
            _mocker.GetMock<IAgendamentoRepository>().Verify(x=> x.SaveChangesAsync(),Times.Once);

            _mocker.GetMock<IItemAgendamentoRepository>().Verify(x=> x.InserirItemAsync(It.IsAny<ItemAgendamento>()),Times.Exactly(5));
            _mocker.GetMock<IItemAgendamentoRepository>().Verify(x=> x.SaveChangesAsync(),Times.Exactly(5));
        }

        [Fact]
        public void AgedamentoCommandSemItens_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var erro = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _novoAgendamentoCommandHandler.Handle(_novoAgendamentoCommand,new CancellationToken()));
            Assert.NotNull(erro.Result);
            Assert.Equal("Nenhum produto informado para realizar o agendamento.",erro.Result.Message);
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