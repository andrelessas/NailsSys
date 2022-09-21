using Bogus;
using Moq;
using NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorData;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterAgendamentosPorDataQueriesHandlerTests:Configurations
    {
        private readonly Mock<IAgendamentoRepository> _agendamentosRepository;
        private readonly ObterAgendamentosPorDataQueriesHandler _obterAgendamentosPorDataQueriesHandler;

        public ObterAgendamentosPorDataQueriesHandlerTests()
        {
            _agendamentosRepository = new Mock<IAgendamentoRepository>();
            _obterAgendamentosPorDataQueriesHandler = new ObterAgendamentosPorDataQueriesHandler(_agendamentosRepository.Object, IMapper);
        }
        [Fact]
        public async Task ObtergendamentosPorData_QuandoExecutado_RetornarListagemsDeAgendamentosAsync()
        {
            //Arrange            
            var listaDeAgendamentos = new List<Agendamento>();
            for (int i = 1; i < 10; i++)
            {
                listaDeAgendamentos.Add(
                    new Agendamento(
                        new Faker().Random.Int(0, 50),
                        new Faker().Date.Recent(),
                        new Faker().Date.Recent(),
                        new Faker().Date.Recent().AddHours(2))   
                );
            }
            _agendamentosRepository.Setup(x => x.ObterAgendamentosPorDataAsync(It.IsAny<DateTime>())).ReturnsAsync(listaDeAgendamentos);

            //Act
            var result = await _obterAgendamentosPorDataQueriesHandler.Handle(new ObterAgendamentosPorDataQueries(DateTime.Now), new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(listaDeAgendamentos.Count(),result.Count());
            foreach (var agendamento in listaDeAgendamentos)
            {
                Assert.Contains<AgendamentoViewModel>(result,x=> x.DataAtendimento == agendamento.DataAtendimento);  
                Assert.Contains<AgendamentoViewModel>(result,x=> x.InicioPrevisto == agendamento.InicioPrevisto);  
                Assert.Contains<AgendamentoViewModel>(result,x=> x.TerminoPrevisto == agendamento.TerminoPrevisto);  
            }
        }

        [Fact]
        public void AgendamentoInvalido_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var excecao = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterAgendamentosPorDataQueriesHandler.Handle(new ObterAgendamentosPorDataQueries(DateTime.Now), new CancellationToken()));
            Assert.NotNull(excecao.Result);
            Assert.Equal("Nenhum agendamento encontrado para a data informada.",excecao.Result.Message);
        }
    }
}