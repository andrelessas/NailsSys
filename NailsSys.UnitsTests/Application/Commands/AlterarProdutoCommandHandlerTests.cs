using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using NailsSys.Application.Commands.ProdutoCommands.AlterarProduto;
using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using Xunit;

namespace NailsSys.UnitsTests.Application.Commands
{
    public class AlterarProdutoCommandHandlerTests
    {
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly AlterarProdutoCommand _produtoCommand;
        private readonly AlterarProdutoCommandHandler _produtoCommandHandler;

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
        }

        [Fact]
        public async void DadoProdutoValido_AlterarCadastroDeProduto()
        {
            //Arrange
            var produto = new Produto(
                new Faker().Commerce.ProductName(),
                "S",
                Convert.ToDecimal(new Faker().Commerce.Price())
            );
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

        [Fact]
        public void DadoProdutoComEntradaInvalida_QuandoExecutado_RetornarExcecoesFluentValidations()
        {
            //Arrange            
            var produtoValidation = new AlterarProdutoCommandValidation();
            //Act
            var result = produtoValidation.Validate(new AlterarProdutoCommand());
            //Assert
            Assert.False(result.IsValid);                  
        }
    }
}