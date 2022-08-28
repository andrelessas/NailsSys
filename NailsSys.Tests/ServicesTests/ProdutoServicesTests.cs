// using AutoMapper;
// using Moq;
// using Moq.AutoMock;
// using NailsSys.Core.DTOs;
// using NailsSys.Core.Interfaces;
// using NailsSys.Core.Models;
// using NailsSys.Core.Notificacoes;
// using NailsSys.Core.Services;
// using NailsSys.Tests.ModelFakes;
// using NailsSys.Tests.ModelValidations;
// using Xunit;

// namespace NailsSys.Tests.ServicesTests
// {
//     public class ProdutoServicesTests
//     {
//         private readonly AutoMocker _mocker;
//         private readonly ProdutoServices _produtoServices;
//         private readonly ProdutoFakers _produtoFaker;
//         private readonly ProdutoValidation _produtoValidate;
//         private readonly ProdutoDTO _produtoDTO;
//         private readonly List<ProdutoDTO> _produtosDTOFakers;
//         private readonly Produto _produto;
//         private readonly IEnumerable<Produto> _listagemProdutos;

//         public ProdutoServicesTests()
//         {
//             _mocker = new AutoMocker();            
//             _produtoServices = _mocker.CreateInstance<ProdutoServices>();
//             _produtoFaker = new ProdutoFakers();
//             _produtoValidate = new ProdutoValidation();
//             _produtoDTO = _produtoFaker.ObterProdutoDTOFaker();
//             _produtosDTOFakers = _produtoFaker.ObterProdutoDTOFaker(10);
//             _produto = new AutoMapperConfiguration().ObterProdutoMapeado(_produtoDTO);
//             _listagemProdutos = new AutoMapperConfiguration().ObterProdutoMapeado(_produtosDTOFakers);
//         }
                
//         [Fact]
//         public async void ProdutoValido_InserirProdutoAsync_RetornaProdutoCadastradoComSucesso()
//         {
//             //Arrange
//             var validate = _produtoValidate.ValidarProduto(_produtoDTO);
//             _mocker.GetMock<IMapper>().Setup(x=>x.Map<Produto>(It.IsAny<ProdutoDTO>())).Returns(_produto);
//             _mocker.GetMock<IProdutoRepository>().Setup(x=>x.SaveChanges()).ReturnsAsync(1);            
//             //Act
//             await _produtoServices.InserirProdutoAsync(_produtoDTO);
//             //Assert
//             _mocker.GetMock<IProdutoRepository>().Verify(x => x.InserirAsync(It.IsAny<Produto>()),Times.Once);
//             _mocker.GetMock<IProdutoRepository>().Verify(x => x.SaveChanges(),Times.Once);
//         }

//         [Fact]
//         public async void ObterProdutosAsync_RetornarListaDeProdutos()
//         {
//             //Arrange
//             _mocker.GetMock<IProdutoRepository>().Setup(x=>x.ObterTodosAsync()).ReturnsAsync(_listagemProdutos);
//             //Act
//             var produtos = await _produtoServices.ObterProdutosAsync();
//             //Assert
//             Assert.NotEmpty(produtos);
//             Assert.NotNull(produtos);
//             Assert.Equal(10,produtos.Count());
//         }

//         [Fact]
//         public void ObterProdutosAsync_NaoFoiEncontradoProdutoRetornarExcecao()
//         {
//             //Arrange - Act - Assert
//             var excecao = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _produtoServices.ObterProdutosAsync());
//             Assert.Equal("Não foi encontrado nenhum produto cadastrado.",excecao.Result.Message);
//             Assert.Equal(404,excecao.Result.StatusCode);
//         }

//         [Fact]
//         public async void IdDoProduto_ObterProdutoPorID_RetornarObjeto()
//         {
//             //Arrange
//             _mocker.GetMock<IProdutoRepository>().Setup(x=>x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(_produto);
//             //Act
//             var produto = await _produtoServices.ObterProdutoPorID(1);
//             //Assert
//             Assert.NotNull(produto);
//             Assert.Equal(_produto.Descricao,produto.Descricao);
//             Assert.Equal(_produto.Preco,produto.Preco);
//             Assert.Equal(_produto.TipoProduto,produto.TipoProduto);
//         }

//         [Fact]
//         public void IdDoProduto_ObterProdutoPorID_ProdutoNaoEncontradoRetornarExcecao()
//         {
//             //Arrange - Act - Assert
//             var excecao = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _produtoServices.ObterProdutoPorID(1));
//             Assert.Equal("Nenhum Produto encontrado.",excecao.Result.Message);
//             Assert.Equal(404,excecao.Result.StatusCode);
//         }
//     }
// }