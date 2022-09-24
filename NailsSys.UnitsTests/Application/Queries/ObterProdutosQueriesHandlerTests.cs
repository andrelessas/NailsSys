using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using Moq;
using NailsSys.Application.Queries.ClienteQueries.ObterClientes;
using NailsSys.Application.Queries.ProdutoQueries.ObterProdutos;
using NailsSys.Application.Validations;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterProdutosQueriesHandlerTests:TestsConfigurations
    {
        private readonly Mock<IProdutoRepository> _produtoRepostory;
        private readonly ObterProdutosQueriesHandler _obterProdutosQueriesHandler;
        private readonly ObterProdutosQueriesValidation _obterProdutosQueriesValidation;

        public ObterProdutosQueriesHandlerTests()
        {
            _produtoRepostory = new Mock<IProdutoRepository>();
            _obterProdutosQueriesHandler = new ObterProdutosQueriesHandler(_produtoRepostory.Object,IMapper);
            _obterProdutosQueriesValidation = new ObterProdutosQueriesValidation();
        }

        [Fact]
        public async Task ObterProdutosValidos_QuandoExecutado_RetornarObjetoAsync()
        {
            //Arrange
            var produtos = AutoFaker.Generate<Produto>(10);            

            var pagination = new AutoFaker<PaginationResult<Produto>>()
                .RuleFor(x=>x.Data,produtos);

            _produtoRepostory.Setup(x=>x.ObterTodosAsync(It.IsAny<int>())).ReturnsAsync(pagination);
            //Act
            var result = await _obterProdutosQueriesHandler.Handle(new ObterProdutosQueries(1),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            foreach (var item in produtos)
            {
                Assert.Contains<ProdutoViewModel>(result.Data,x=>x.Descontinuado == item.Descontinuado);
                Assert.Contains<ProdutoViewModel>(result.Data,x=>x.Descricao == item.Descricao);
                Assert.Contains<ProdutoViewModel>(result.Data,x=>x.Preco == item.Preco);
                Assert.Contains<ProdutoViewModel>(result.Data,x=>x.TipoProduto == item.TipoProduto);                
            }
        }

        [Fact]
        public void Invalido_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterProdutosQueriesHandler.Handle(new ObterProdutosQueries(2), new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.Equal("Nenhum produto encontrado.",result.Result.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void PageInvalido_RetornarExcecoesFluentValidations(int page)
        {
            //Arrange - Act
            var result = _obterProdutosQueriesValidation.Validate(new ObterProdutosQueries(page));
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensPaginacao.PageMaiorQueZero) ||
                        erros.Contains(MensagensPaginacao.PageNullVazio));
        }
    }
}