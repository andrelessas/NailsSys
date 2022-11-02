using Bogus;
using Moq;
using NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItemPorId;
using NailsSys.Core.DTOs;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterItemPorIdQueriesHandlerTests:TestsConfigurations
    {
        private readonly Mock<IItemAgendamentoRepository> _itemAgendamentoRepository;
        private readonly ObterItemPorIdQueriesHandler _obterItemPorIdQueriesHandler;

        public ObterItemPorIdQueriesHandlerTests()
        {
            _itemAgendamentoRepository = new Mock<IItemAgendamentoRepository>();
            _obterItemPorIdQueriesHandler = new ObterItemPorIdQueriesHandler(_itemAgendamentoRepository.Object,IMapper);
        }

        [Fact]
        public async void IdClienteValido_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            var item = new ItemAgendamentoDTO(1,
                1,
                1,
                new Faker().Commerce.ProductName(),
                1,
                Convert.ToDecimal(new Faker().Commerce.Price()),
                1);
                
            _itemAgendamentoRepository.Setup(x=> x.ObterItemPorId(It.IsAny<int>())).ReturnsAsync(item);
            //Act
            var result = await _obterItemPorIdQueriesHandler.Handle(new ObterItemPorIdQueries(1),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(item.DescricaoProduto,result.NomeProduto);
            Assert.Equal(item.Item,result.Item);            
            Assert.Equal(item.Quantidade,result.Quantidade);
        }

        [Fact]
        public void ClienteNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterItemPorIdQueriesHandler.Handle(new ObterItemPorIdQueries(2), new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.Equal("Item não encontrado.",result.Result.Message);
        }
    }
}