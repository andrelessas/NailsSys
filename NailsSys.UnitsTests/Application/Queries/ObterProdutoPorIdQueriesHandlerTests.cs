using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using Moq;
using NailsSys.Application.Queries.ProdutoQueries.ObterProdutoPorId;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterProdutoPorIdQueriesHandlerTests:TestsConfigurations
    {
        private readonly Mock<IProdutoRepository> _produtoRepository;
        private readonly ObterProdutoPorIdQueriesHandler _obterProdutoPorIdQueriesHandler;
        private readonly ObterProdutoPorIdQueriesValidation _obterProdutoPorIdQueriesValidation;

        public ObterProdutoPorIdQueriesHandlerTests()
        {
            _produtoRepository = new Mock<IProdutoRepository>();
            _obterProdutoPorIdQueriesHandler = new ObterProdutoPorIdQueriesHandler(_produtoRepository.Object,IMapper);
            _obterProdutoPorIdQueriesValidation = new ObterProdutoPorIdQueriesValidation();
        }    

        [Fact]
        public async Task IdProdutoValido_QuandoExecutado_RetornarObjetoAsync()
        {
            //Arrange
            var produto = AutoFaker.Generate<Produto>();
            _produtoRepository.Setup(x=>x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(produto);
            //Act
            var result = await _obterProdutoPorIdQueriesHandler.Handle(new ObterProdutoPorIdQueries(1),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(produto.Descricao,result.Descricao);
            Assert.Equal(produto.TipoProduto,result.TipoProduto);
            Assert.Equal(produto.Preco,result.Preco);
            Assert.Equal(produto.Descontinuado,result.Descontinuado);
        }

        [Fact]
        public void ProdutoNaoEncontrado_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterProdutoPorIdQueriesHandler.Handle(new ObterProdutoPorIdQueries(1),new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.Equal("Produto não encontrado.",result.Result.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void MudarNomeDoMetodo(int id)
        {
            //Arrange - Act
            var result = _obterProdutoPorIdQueriesValidation.Validate(new ObterProdutoPorIdQueries(id));
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains("Necessário informar o Id do produto.") ||
                        erros.Contains("O Id do produto deve ser maior que 0."));
        }
    }
}