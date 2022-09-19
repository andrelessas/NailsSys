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
            await _obterAgendamentosHojeQueriesHandler.Handle(new ObterAgendamentosHojeQueries(), new CancellationToken());
            //Assert
        }
    }
}