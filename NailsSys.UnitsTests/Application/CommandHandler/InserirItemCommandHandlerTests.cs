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
    public class InserirItemCommandHandlerTests : TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private readonly Mock<IItemAgendamentoRepository> _itemAgendamentoRepository;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly InserirItemCommand _inserirItemCommand;
        private readonly InserirItemCommandHandler _inserirItemCommandHandler;
        private readonly InserirItemCommandValidation _itenserirItemCommandValidate;

        public InserirItemCommandHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
            _itemAgendamentoRepository = new Mock<IItemAgendamentoRepository>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _unitOfWorks.SetupGet(x => x.ItemAgendamento).Returns(_itemAgendamentoRepository.Object);
            _unitOfWorks.SetupGet(x => x.Produto).Returns(_produtoRepository.Object);
            _inserirItemCommand = new Faker<InserirItemCommand>()
                .RuleFor(i => i.IdAgendamento, v => v.Random.Int(1, 50))
                .RuleFor(i => i.IdProduto, v => v.Random.Int(1, 50))
                .RuleFor(i => i.Quantidade, v => v.Random.Int(1, 10))
                .Generate();

            _inserirItemCommandHandler = new InserirItemCommandHandler(_unitOfWorks.Object);
            _itenserirItemCommandValidate = new InserirItemCommandValidation();
        }

        [Fact]
        public async Task ItemValido_QuandoExcecutado_InserirNovoItemNoAgendamentoAsync()
        {
            //Arrange
            var produto = AutoFaker.Generate<Produto>();
            _itemAgendamentoRepository.Setup(it => it.ObterMaxItem(It.IsAny<int>())).ReturnsAsync(2);
            _produtoRepository.Setup(pr => pr.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(produto);
            //Act
            await _inserirItemCommandHandler.Handle(_inserirItemCommand, new CancellationToken());
            //Assert
            _itemAgendamentoRepository.Verify(it => it.InserirItemAsync(It.IsAny<ItemAgendamento>()), Times.Once);
            _unitOfWorks.Verify(it => it.SaveChangesAsync(), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        [InlineData(-10)]
        public void ValidarIdProdutoInvalido_RetornarExcecaoFluentValidation(int idProduto)
        {
            //Arrange
            var itemAgendamentoCommand = new InserirItemCommand { IdProduto = idProduto };
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
            var itemAgendamentoCommand = new InserirItemCommand { IdAgendamento = idAgendamento };
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
            var itemAgendamentoCommand = new InserirItemCommand { Quantidade = quantidade };
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