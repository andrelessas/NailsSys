using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorPeriodoDoDia
{
    public class ObterAgendamentosPorPeriodoDoDiaQueriesHandler : IRequestHandler<ObterAgendamentosPorPeriodoDoDiaQueries, IEnumerable<AgendamentoViewModel>>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IMapper _mapper;

        public ObterAgendamentosPorPeriodoDoDiaQueriesHandler(IAgendamentoRepository agendamentoRepository,
                                                              IMapper mapper)
        {
            _agendamentoRepository = agendamentoRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AgendamentoViewModel>> Handle(ObterAgendamentosPorPeriodoDoDiaQueries request, CancellationToken cancellationToken)
        {
            var agendamentos = await _agendamentoRepository.ObterAgendamentosPorPeriodoDoDiaAsync(request.PeriodoInicial, request.PeriodoFinal);

            if(agendamentos.Count() == 0)
                throw new ExcecoesPersonalizadas("Nenhum agendamento encontrado dentro do período informado.");

            return _mapper.Map<IEnumerable<AgendamentoViewModel>>(agendamentos);
        }
    }
}