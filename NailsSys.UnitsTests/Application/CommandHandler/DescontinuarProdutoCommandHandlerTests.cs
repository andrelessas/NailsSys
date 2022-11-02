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
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class DescontinuarProdutoCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private readonly DescontinuarProdutoCommand _produtoCommand;
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly DescontinuarProdutoCommandHandler _descontinuarProdutoCommandHandler;

        public DescontinuarProdutoCommandHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
            _produtoCommand = new DescontinuarProdutoCommand(1);
            _produtoRepository = new Mock<IProdutoRepository>();
            _unitOfWorks.SetupGet(x => x.Produto).Returns(_produtoRepository.Object);
            _descontinuarProdutoCommandHandler = new DescontinuarProdutoCommandHandler(_unitOfWorks.Object);
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
            _unitOfWorks.Verify(pr => pr.SaveChangesAsync(),Times.Once);
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
        public void IdProdutoInvalido_RetornarExcecaoFluentValidation(int idProduto)
        {
            //Arrange
            var descontinuarProdutoCommandValidation = new DescontinuarProdutoCommandValidation();
            var descontinuarProdutoCommand = new DescontinuarProdutoCommand(idProduto);
            //Act
            var result = descontinuarProdutoCommandValidation.Validate(descontinuarProdutoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensProduto.IdProdutoNaoInformadoParaBloqueioProduto) ||
                        erros.Contains(MensagensProduto.IdProdutoMaiorQueZero));
        }
    }
}