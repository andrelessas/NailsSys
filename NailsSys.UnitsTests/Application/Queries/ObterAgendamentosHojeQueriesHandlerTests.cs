using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosHoje;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterAgendamentosHojeQueriesHandlerTests : Configurations
    {
        private readonly Mock<IAgendamentoRepository> _agendamentosRepository;
        private readonly ObterAgendamentosHojeQueriesHandler _obterAgendamentosHojeQueriesHandler;

        public ObterAgendamentosHojeQueriesHandlerTests()
        {
            _agendamentosRepository = new Mock<IAgendamentoRepository>();
            _obterAgendamentosHojeQueriesHandler = new ObterAgendamentosHojeQueriesHandler(_agendamentosRepository.Object, IMapper);
        }

        [Fact]
        public async Task ObtergendamentosHoje_QuandoExecutado_RetornarListagemsDeAgendamentosAsync()
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
            _agendamentosRepository.Setup(x => x.ObterAgendamentosHojeAsync()).ReturnsAsync(listaDeAgendamentos);

            //Act
            var result = await _obterAgendamentosHojeQueriesHandler.Handle(new ObterAgendamentosHojeQueries(), new CancellationToken());
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
            var excecao = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterAgendamentosHojeQueriesHandler.Handle(new ObterAgendamentosHojeQueries(), new CancellationToken()));
            Assert.NotNull(excecao.Result);
            Assert.Equal("Nenhum agendamento encontrado para hoje.",excecao.Result.Message);
        }
    }
}