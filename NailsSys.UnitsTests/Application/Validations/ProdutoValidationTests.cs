using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;
using NailsSys.Application.Validations;
using Xunit;

namespace NailsSys.UnitsTests.Application.Validations
{
    public class ProdutoValidationTests
    {
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
            Assert.True(erros.Contains("Informe o nome do produto.") == true || 
                        erros.Contains("Descrição do produto deve ter no máximo 50 caracteres.") == true);        
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
            Assert.True(erros.Contains("Necessário informar o tipo do produto, se é S - Serviço ou P - Produto.") == true ||
                        erros.Contains("Necessário informar o tipo do produto.") == true);        
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
            Assert.True(erros.Contains("Informe o preço de venda do produto.") == true);        
        }
    }
}