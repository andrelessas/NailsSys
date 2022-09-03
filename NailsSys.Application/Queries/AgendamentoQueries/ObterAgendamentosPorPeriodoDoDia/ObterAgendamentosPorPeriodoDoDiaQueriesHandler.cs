using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

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
            return _mapper.Map<IEnumerable<AgendamentoViewModel>>(await _agendamentoRepository.ObterAgendamentosPorPeriodoDoDiaAsync(request.PeriodoInicial, request.PeriodoFinal));
        }
    }
}