using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosHoje
{
    public class ObterAgendamentosHojeQueriesHandler : IRequestHandler<ObterAgendamentosHojeQueries, IEnumerable<AgendamentoViewModel>>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IMapper _mapper;

        public ObterAgendamentosHojeQueriesHandler(IAgendamentoRepository agendamentoRepository,IMapper mapper)
        {
            _agendamentoRepository = agendamentoRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AgendamentoViewModel>> Handle(ObterAgendamentosHojeQueries request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Agendamento>,IEnumerable<AgendamentoViewModel>>(await _agendamentoRepository.ObterAgendamentosHojeAsync());
        }
    }
}