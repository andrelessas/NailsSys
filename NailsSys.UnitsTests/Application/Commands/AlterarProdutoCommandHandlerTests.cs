using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using NailsSys.Application.Commands.ProdutoCommands.AlterarProduto;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using Xunit;

namespace NailsSys.UnitsTests.Application.Commands
{
    public class AlterarProdutoCommandHandlerTests
    {
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly AlterarProdutoCommand _produtoCommand;

        public AlterarProdutoCommandHandlerTests()
        {
            _produtoRepository = new Mock<IProdutoRepository>();
            _produtoCommand = new Faker<AlterarProdutoCommand>()
                .RuleFor(p=> p.Id, 1)
                .RuleFor(p=> p.Descricao, f=> f.Commerce.ProductName())
                .RuleFor(p=> p.Preco, f=> Convert.ToDecimal(f.Commerce.Price()))
                .RuleFor(p=> p.TipoProduto, "S")
                .Generate();                
        }

        [Fact]
        public async void DadoProdutoValidos_AlterarCadastroDeProduto()
        {
            //Arrange
            var produtoCommandHandler = new AlterarProdutoCommandHandler(_produtoRepository.Object);
            var produto = new Produto(
                new Faker().Commerce.ProductName(),
                "S",
                Convert.ToDecimal(new Faker().Commerce.Price())
            );
            _produtoRepository.Setup(x => x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(produto);
            //Act
            await produtoCommandHandler.Handle(_produtoCommand,new CancellationToken());
            //Assert
            _produtoRepository.Verify(x => x.ObterPorIDAsync(It.IsAny<int>()),Times.Once);
            _produtoRepository.Verify(x => x.SaveChangesAsync(),Times.Once);
        }
    }
}