using AutoBogus;
using Bogus;
using Moq;
using NailsSys.Application.Commands.ProdutoCommands.AlterarProduto;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class AlterarProdutoCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly AlterarProdutoCommand _produtoCommand;
        private readonly AlterarProdutoCommandHandler _produtoCommandHandler;
        private readonly AlterarProdutoCommandValidation _alterarProdutoValidation;

        public AlterarProdutoCommandHandlerTests()
        {
            _produtoRepository = new Mock<IProdutoRepository>();
            _produtoCommand = new Faker<AlterarProdutoCommand>()
                .RuleFor(p=> p.Id, 1)
                .RuleFor(p=> p.Descricao, f=> f.Commerce.ProductName())
                .RuleFor(p=> p.Preco, f=> Convert.ToDecimal(f.Commerce.Price()))
                .RuleFor(p=> p.TipoProduto, "S")
                .Generate(); 

            _produtoCommandHandler = new AlterarProdutoCommandHandler(_produtoRepository.Object); 
            _alterarProdutoValidation = new AlterarProdutoCommandValidation();
        }

        [Fact]
        public async void DadoProdutoValido_AlterarCadastroDeProduto()
        {
            //Arrange
            var produto = AutoFaker.Generate<Produto>();
            _produtoRepository.Setup(x => x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(produto);
            //Act
            await _produtoCommandHandler.Handle(_produtoCommand,new CancellationToken());
            //Assert
            _produtoRepository.Verify(x => x.ObterPorIDAsync(It.IsAny<int>()),Times.Once);
            _produtoRepository.Verify(x => x.SaveChangesAsync(),Times.Once);
        }

        [Fact]
        public void DadoProdutoNaoEncontrado_QuandoExecutado_RetornarExcecaoAsync()
        {
            //Arrange - Act - Assert
            var excecao = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _produtoCommandHandler.Handle(_produtoCommand,new CancellationToken())).Result;            
            Assert.Equal("Produto não encontrado.",excecao.Message);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TestarDescricaoProdutoInvalida_RetornarExcecoesFluentValidations(string descricao)
        {
            //Arrange
            var produtoCommand = new AlterarProdutoCommand{Descricao = descricao};
            //Act
            var result = _alterarProdutoValidation.Validate(produtoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensProduto.DescricaoNullVazio) || 
                        erros.Contains(MensagensProduto.LimiteTamanhoDescricao));
        }
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("null")]
        public void TestarTipoProdutoInvalido_RetornarExcecoesFluentValidations(string tipoProduto)
        {
            //Arrange
            var produtoCommand = new AlterarProdutoCommand{TipoProduto = tipoProduto};
            //Act
            var result = _alterarProdutoValidation.Validate(produtoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensProduto.TipoProdutoNullVazio) ||
                        erros.Contains(MensagensProduto.ValidarTipoProduto));        
        }
        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        public void TestarPrecoProdutoInvalido_RetornarExcecoesFluentValidations(decimal preco)
        {
            //Arrange
            var produtoCommand = new AlterarProdutoCommand{Preco = preco};
            //Act
            var result = _alterarProdutoValidation.Validate(produtoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensProduto.PrecoMaiorQueZero) ||
                        erros.Contains(MensagensProduto.PrecoNullVazio));
        }
    }
}