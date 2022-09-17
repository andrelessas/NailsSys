using Bogus;
using Moq;
using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class InserirProdutoCommandHandlerTests
    {
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly InserirProdutoCommand _produtoCommand;
        private readonly InserirProdutoCommandHandler _inserirProdutoCommandHandler;

        public InserirProdutoCommandHandlerTests()
        {
            _produtoRepository = new Mock<IProdutoRepository>();
            _produtoCommand = new Faker<InserirProdutoCommand>()                
                .RuleFor(p=> p.Descricao, f=> f.Commerce.ProductName())
                .RuleFor(p=> p.Preco, f=> Convert.ToDecimal(f.Commerce.Price()))
                .RuleFor(p=> p.TipoProduto, "S")
                .Generate(); 

            _inserirProdutoCommandHandler = new InserirProdutoCommandHandler(_produtoRepository.Object);
        }        

        [Fact]
        public async Task DadoProdutoValido_AoExecutar_InserirProdutoAsync()
        {
            //Arrange - Act
            await _inserirProdutoCommandHandler.Handle(_produtoCommand,new CancellationToken());
            //Assert
            _produtoRepository.Verify(x=> x.InserirAsync(It.IsAny<Produto>()),Times.Once);
            _produtoRepository.Verify(x=> x.SaveChangesAsync(),Times.Once);
        }   

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestarDescricaoProdutoInvalida_RetornarExcecoesFluentValidations(string descricao)
        {
            //Arrange
            var produtoCommand = new InserirProdutoCommand{Descricao = descricao};

            var validate = new InserirProdutoCommandValidation();            
            //Act
            var result = validate.Validate(produtoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(m => m.ErrorMessage).ToList();
            Assert.True(erros.Contains("Informe o nome do produto.") || 
                        erros.Contains("Descrição do produto deve ter no máximo 50 caracteres."));        
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("null")]
        public void TestarTipoProdutoInvalido_RetornarExcecoesFluentValidations(string tipoProduto)
        {
            //Arrange
            var produtoCommand = new InserirProdutoCommand{TipoProduto = tipoProduto};

            var validate = new InserirProdutoCommandValidation();            
            //Act
            var result = validate.Validate(produtoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(m => m.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar o tipo do produto, se é S - Serviço ou P - Produto.") ||
                        erros.Contains("Necessário informar o tipo do produto."));        
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void TestarPrecoProdutoInvalido_RetornarExcecoesFluentValidations(decimal preco)
        {
            //Arrange
            var produtoCommand = new InserirProdutoCommand{Preco = preco};

            var validate = new InserirProdutoCommandValidation();            
            //Act
            var result = validate.Validate(produtoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(m => m.ErrorMessage).ToList();
            Assert.True(erros.Contains("Informe o preço de venda do produto."));        
        } 
    }
}