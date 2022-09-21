using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

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
            var agendamentos = await _agendamentoRepository.ObterAgendamentosHojeAsync();

            if(agendamentos.Count() == 0)
                throw new ExcecoesPersonalizadas("Nenhum agendamento encontrado para hoje.");

            return _mapper.Map<IEnumerable<AgendamentoViewModel>>(agendamentos);
        }
    }
}