using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorPeriodoDoDia
{
    public class ObterAgendamentosPorPeriodoDoDiaQueriesHandler : IRequestHandler<ObterAgendamentosPorPeriodoDoDiaQueries, IEnumerable<AgendamentoViewModel>>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;

        public ObterAgendamentosPorPeriodoDoDiaQueriesHandler(IAgendamentoRepository agendamentoRepository,IItemAgendamentoRepository itemAgendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _itemAgendamentoRepository = itemAgendamentoRepository;   
        }
        public async Task<IEnumerable<AgendamentoViewModel>> Handle(ObterAgendamentosPorPeriodoDoDiaQueries request, CancellationToken cancellationToken)
        {
            var agendamentos = await _agendamentoRepository.ObterAgendamentosPorPeriodoDoDiaAsync(request.PeriodoInicial,request.PeriodoFinal);

            foreach (var agendamento in agendamentos)
            {
                var itens = await _itemAgendamentoRepository.ObterItensAsync(agendamento.Id);
                itens.Select(i => new ItemAgendamentoViewModel(i.DescricaoProduto,i.Quantidade,i.PrecoInicial,i.Id));
            }

            // return agendamentos.Select( a => new AgendamentoViewModel(a.DataAtendimento,a.InicioPrevisto,a.TerminoPrevisto,a.Cliente.NomeCliente));
            return null;
        }
    }
}