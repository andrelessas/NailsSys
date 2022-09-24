using AutoBogus;
using Bogus;
using Moq;
using Moq.AutoMock;
using NailsSys.Application.Commands.ItemAgendamentoCommands.InserirItemCommand;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class InserirItemCommandHandlerTests:TestsConfigurations
    {
        private readonly AutoMocker _mocker;
        private readonly InserirItemCommand _inserirItemCommand;
        private readonly InserirItemCommandHandler _inserirItemCommandHandler;
        private readonly InserirItemCommandValidation _itenserirItemCommandValidate;

        public InserirItemCommandHandlerTests()
        {
            _mocker = new AutoMocker();

            _inserirItemCommand = new Faker<InserirItemCommand>()
                .RuleFor(i => i.IdAgendamento, v => v.Random.Int(1,50))
                .RuleFor(i => i.IdProduto, v => v.Random.Int(1,50))
                .RuleFor(i => i.Quantidade, v => v.Random.Int(1,10))
                .Generate();

            _inserirItemCommandHandler = _mocker.CreateInstance<InserirItemCommandHandler>();
            _itenserirItemCommandValidate = new InserirItemCommandValidation();
        }

        [Fact]
        public async Task ItemValido_QuandoExcecutado_InserirNovoItemNoAgendamentoAsync()
        {
            //Arrange
            var produto = AutoFaker.Generate<Produto>();
            _mocker.GetMock<IItemAgendamentoRepository>().Setup(it => it.ObterMaxItem(It.IsAny<int>())).ReturnsAsync(2);
            _mocker.GetMock<IProdutoRepository>().Setup(pr => pr.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(produto);
            //Act
            await _inserirItemCommandHandler.Handle(_inserirItemCommand,new CancellationToken());
            //Assert
            _mocker.GetMock<IItemAgendamentoRepository>().Verify(it => it.InserirItemAsync(It.IsAny<ItemAgendamento>()),Times.Once);
            _mocker.GetMock<IItemAgendamentoRepository>().Verify(it => it.SaveChangesAsync(),Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        [InlineData(-10)]
        public void ValidarIdProdutoInvalido_RetornarExcecaoFluentValidation(int idProduto)
        {
            //Arrange
            var itemAgendamentoCommand = new InserirItemCommand{IdProduto = idProduto};
            //Act
            var result = _itenserirItemCommandValidate.Validate(itemAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensItensAgendamento.IdProdutoMaiorQueZero) ||
                        erros.Contains(MensagensItensAgendamento.IdProdutoNullVazio));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        [InlineData(-10)]
        public void ValidarIdAgendamentoInvalido_RetornarExcecaoFluentValidation(int idAgendamento)
        {
            //Arrange
            var itemAgendamentoCommand = new InserirItemCommand{IdAgendamento = idAgendamento};
            //Act
            var result = _itenserirItemCommandValidate.Validate(itemAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensItensAgendamento.IdAgendamentoMaiorQueZero) ||
                        erros.Contains(MensagensItensAgendamento.IdAgendamentoNullVazio));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        [InlineData(-10)]
        public void ValidarQuantidadeInvalida_RetornarExcecaoFluentValidation(int quantidade)
        {
            //Arrange
            var itemAgendamentoCommand = new InserirItemCommand{Quantidade = quantidade};
            //Act
            var result = _itenserirItemCommandValidate.Validate(itemAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensItensAgendamento.QuantidadeMaiorQueZero) ||
                        erros.Contains(MensagensItensAgendamento.QuantidadeNullVazio));
        }
    }
}