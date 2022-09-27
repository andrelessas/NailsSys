using Bogus;
using Moq;
using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class InserirProdutoCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly InserirProdutoCommand _produtoCommand;
        private readonly InserirProdutoCommandHandler _inserirProdutoCommandHandler;

        public InserirProdutoCommandHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
            _produtoRepository = new Mock<IProdutoRepository>();
            _produtoCommand = new Faker<InserirProdutoCommand>()                
                .RuleFor(p=> p.Descricao, f=> f.Commerce.ProductName())
                .RuleFor(p=> p.Preco, f=> Convert.ToDecimal(f.Commerce.Price()))
                .RuleFor(p=> p.TipoProduto, "S")
                .Generate(); 

            _unitOfWorks.SetupGet(x => x.Produto).Returns(_produtoRepository.Object);

            _inserirProdutoCommandHandler = new InserirProdutoCommandHandler(_unitOfWorks.Object);
        }        

        [Fact]
        public async Task DadoProdutoValido_AoExecutar_InserirProdutoAsync()
        {
            //Arrange - Act
            await _inserirProdutoCommandHandler.Handle(_produtoCommand,new CancellationToken());
            //Assert
            _produtoRepository.Verify(x=> x.InserirAsync(It.IsAny<Produto>()),Times.Once);
            _unitOfWorks.Verify(x=> x.SaveChangesAsync(),Times.Once);
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
            var produtoCommand = new InserirProdutoCommand{TipoProduto = tipoProduto};

            var validate = new InserirProdutoCommandValidation();            
            //Act
            var result = validate.Validate(produtoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensProduto.TipoProdutoNullVazio) ||
                        erros.Contains(MensagensProduto.DescricaoNullVazio));        
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
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensProduto.PrecoMaiorQueZero) ||
                        erros.Contains(MensagensProduto.PrecoNullVazio));        
        } 
    }
}