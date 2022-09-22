using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using Moq;
using NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItens;
using NailsSys.Application.Validations;
using NailsSys.Application.ViewModels;
using NailsSys.Core.DTOs;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterItensQueriesHandlerTests : TestsConfigurations
    {
        private readonly Mock<IItemAgendamentoRepository> _itensAgendamentoRepository;
        private readonly ObterItensQueriesHandler _obterItensQueriesHandler;
        private readonly ObterItensQueriesValidation _obterItensQueriesValidation;

        public ObterItensQueriesHandlerTests()
        {
            _itensAgendamentoRepository = new Mock<IItemAgendamentoRepository>();
            _obterItensQueriesHandler = new ObterItensQueriesHandler(_itensAgendamentoRepository.Object, IMapper);
            _obterItensQueriesValidation = new ObterItensQueriesValidation();
        }

        [Fact]
        public async void ObterItens_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            var itens = AutoFaker.Generate<ItemAgendamentoDTO>(5);
            var pagination = new AutoFaker<PaginationResult<ItemAgendamentoDTO>>()
                .RuleFor(x => x.Data, itens)
                .Generate();

            _itensAgendamentoRepository.Setup(x => x.ObterItensAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(pagination);
            //Act
            var result = await _obterItensQueriesHandler.Handle(new ObterItensQueries(1, 1), new CancellationToken());
            //Assert
            Assert.NotNull(result);
            foreach (var item in itens)
            {
                Assert.Contains<ItemAgendamentoViewModel>(result.Data, x => x.Item == item.Item);
                Assert.Contains<ItemAgendamentoViewModel>(result.Data, x => x.NomeProduto == item.DescricaoProduto);
                Assert.Contains<ItemAgendamentoViewModel>(result.Data, x => x.PrecoInicial == item.PrecoInicial);
                Assert.Contains<ItemAgendamentoViewModel>(result.Data, x => x.Quantidade == item.Quantidade);
            }
        }

        [Fact]
        public void ItemInvalido_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterItensQueriesHandler.Handle(new ObterItensQueries(1, 1), new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.Equal("Nenhum item encontrado.", result.Result.Message);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        public void ParametrosDeConsultaInvalido_RetornarExcecaoFluentValidation(int idAgendamento, int page)
        {
            //Arrange - Act
            var result = _obterItensQueriesValidation.Validate(new ObterItensQueries(idAgendamento, page));
            //Assert
            Assert.NotNull(result);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains("Necessário informar o id Agendamento para obter os itens.") || 
                        erros.Contains("O id Agendamento deve ser maior qur 0.") ||
                        erros.Contains("Necessário informar a quantidade de páginas para que seja realizada a paginação dos dados."));
        }
    }
}