using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using NailsSys.Application.Commands.ProdutoCommands.DescontinuarProduto;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class DescontinuarProdutoCommandHandlerTests
    {
        private readonly DescontinuarProdutoCommand _produtoCommand;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly DescontinuarProdutoCommandHandler _descontinuarProdutoCommandHandler;

        public DescontinuarProdutoCommandHandlerTests()
        {
            _produtoCommand = new DescontinuarProdutoCommand(1);
            _produtoRepository = new Mock<IProdutoRepository>();
            _descontinuarProdutoCommandHandler = new DescontinuarProdutoCommandHandler(_produtoRepository.Object);
        }
        [Fact]
        public async void ProdutoValido_QuandoExecutado_DescontinuarProduto()
        {
            //Arrange
            var produto = new Produto(
                new Faker().Commerce.ProductName(),
                "S",
                Convert.ToDecimal(new Faker().Commerce.Price()));

            _produtoRepository.Setup(pr => pr.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(produto);
            //Act
            await _descontinuarProdutoCommandHandler.Handle(_produtoCommand,new CancellationToken());
            //Assert
            _produtoRepository.Verify(pr => pr.SaveChangesAsync(),Times.Once);
        }

        [Fact]
        public void ProdutoInvalido_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _descontinuarProdutoCommandHandler.Handle(_produtoCommand,new CancellationToken()));
            Assert.Equal("Produto não encontrado.", result.Result.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        [InlineData(-10)]
        public void MudarNomedoMetodo(int idProduto)
        {
            //Arrange
            var descontinuarProdutoCommandValidation = new DescontinuarProdutoCommandValidation();
            var descontinuarProdutoCommand = new DescontinuarProdutoCommand(idProduto);
            //Act
            var result = descontinuarProdutoCommandValidation.Validate(descontinuarProdutoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar o Id do produto que será descontinuado.") == true ||
                        erros.Contains("O Id do produto deve ser maior que 0."));
        }
    }
}