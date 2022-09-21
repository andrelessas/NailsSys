using AutoBogus;
using Moq;
using NailsSys.Application.Queries.ClienteQueries.ObterClientes;
using NailsSys.Application.Validations;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Core.Notificacoes;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterClientesQueriesHandlerTests:Configurations
    {
        private readonly Mock<IClienteRepository> _clienteRepository;
        private readonly ObterClientesQueriesHandler _obterClientesQueriesHandler;
        private readonly ObterClientesQueriesValidation _obterClientesQueriesHandlerValidation;

        public ObterClientesQueriesHandlerTests()
        {
            _clienteRepository = new Mock<IClienteRepository>();
            _obterClientesQueriesHandler = new ObterClientesQueriesHandler(_clienteRepository.Object,IMapper);
            _obterClientesQueriesHandlerValidation = new ObterClientesQueriesValidation();
        }

        [Fact]
        public async Task ObterClientes_QuandoExecutado_RetornarListaDeClientesAsync()
        {
            //Arrange            
            var clienteFaker = new AutoFaker<Cliente>()
                .RuleFor(x=> x.Id,y=> y.Random.Int(0,20))
                .RuleFor(x=> x.DataCadastro,y=> y.Date.Future(-10))
                .RuleFor(x=> x.NomeCliente,y=> y.Person.FullName)
                .RuleFor(x=> x.Telefone,y=> y.Phone.PhoneNumber())
                .Generate(5);

            var paginationResult = new AutoFaker<PaginationResult<Cliente>>()
                .RuleFor(x=> x.Data,clienteFaker)
                .RuleFor(x=> x.Page,y=>y.Random.Int(0,5))
                .RuleFor(x=> x.ItemsCount,y=>y.Random.Int(0,5))
                .RuleFor(x=> x.PageSize,y=>y.Random.Int(0,5))
                .RuleFor(x=> x.TotalPages,y=>y.Random.Int(0,5))
                .Generate();

            _clienteRepository.Setup(x=> x.ObterTodosAsync(It.IsAny<int>())).ReturnsAsync(paginationResult);
            //Act
            var paginations = await _obterClientesQueriesHandler.Handle(new ObterClientesQueries(5), new CancellationToken());
            //Assert
            Assert.NotNull(paginations);
            Assert.Equal(clienteFaker.Count(),paginations.Data.Count());
            foreach (var cliente in clienteFaker)
            {
                Assert.Contains<ClienteViewModel>(paginations.Data,t => t.Telefone == cliente.Telefone);
                Assert.Contains<ClienteViewModel>(paginations.Data,t => t.NomeCliente == cliente.NomeCliente);
            }
        }

        [Fact]
        public void ClientesNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var erro = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterClientesQueriesHandler.Handle(new ObterClientesQueries(5),new CancellationToken()));
            Assert.NotNull(erro.Result);
            Assert.Equal("Nenhum cliente encontrado.",erro.Result.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void MudarNomeDoMetodo(int page)
        {
            //Arrange - Act
            var result = _obterClientesQueriesHandlerValidation.Validate(new ObterClientesQueries(page));
            //Assert
            Assert.NotNull(result);
            foreach (var erro in result.Errors)
            {
                Assert.Equal("Necessário informar a quantidade de páginas para que seja realizada a paginação dos dados.",erro.ErrorMessage);
            }
            
        }
    }
}