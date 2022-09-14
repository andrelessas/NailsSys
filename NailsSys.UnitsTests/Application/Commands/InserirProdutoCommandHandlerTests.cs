using Bogus;
using Moq;
using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using Xunit;

namespace NailsSys.UnitsTests.Application.Commands
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
    }
}