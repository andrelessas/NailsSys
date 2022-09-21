using Bogus;
using Moq;
using NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorPeriodoDoDia;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterAgendamentosPorPeriodoDoDiaQueriesHandlerTests:Configurations
    {
        private readonly Mock<IAgendamentoRepository> _agendamentoRepository;
        private readonly ObterAgendamentosPorPeriodoDoDiaQueriesHandler _obterAgendamentoPorPeriodoQueriesHandler;

        public ObterAgendamentosPorPeriodoDoDiaQueriesHandlerTests()
        {
            _agendamentoRepository = new Mock<IAgendamentoRepository>();
            _obterAgendamentoPorPeriodoQueriesHandler = new ObterAgendamentosPorPeriodoDoDiaQueriesHandler(_agendamentoRepository.Object,IMapper);
        }    

        [Fact]
        public async Task ObtergendamentosPorPeriodo_QuandoExecutado_RetornarListagemsDeAgendamentosAsync()
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
            _agendamentoRepository.Setup(x => x.ObterAgendamentosPorPeriodoDoDiaAsync(It.IsAny<DateTime>(),It.IsAny<DateTime>())).ReturnsAsync(listaDeAgendamentos);

            //Act
            var result = await _obterAgendamentoPorPeriodoQueriesHandler.Handle(new ObterAgendamentosPorPeriodoDoDiaQueries(DateTime.Now,DateTime.Now.AddHours(5)), new CancellationToken());
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
            var excecao = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterAgendamentoPorPeriodoQueriesHandler.Handle(new ObterAgendamentosPorPeriodoDoDiaQueries(DateTime.Now,DateTime.Now.AddHours(5)), new CancellationToken()));
            Assert.NotNull(excecao.Result);
            Assert.Equal("Nenhum agendamento encontrado dentro do período informado.",excecao.Result.Message);
        }

    }
}