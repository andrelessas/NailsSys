using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorData
{
    public class ObterAgendamentosPorDataQueriesHandler : IRequestHandler<ObterAgendamentosPorDataQueries, IEnumerable<AgendamentoViewModel>>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IMapper _mapper;

        public ObterAgendamentosPorDataQueriesHandler(IAgendamentoRepository agendamentoRepository,
                                                      IMapper mapper)
        {
            _agendamentoRepository = agendamentoRepository;
            _mapper = mapper;
        }    
        public async Task<IEnumerable<AgendamentoViewModel>> Handle(ObterAgendamentosPorDataQueries request, CancellationToken cancellationToken)
        {
            var agendamentos = await _agendamentoRepository.ObterAgendamentosPorDataAsync(request.Data);

            if(agendamentos.Count() == 0)
                throw new ExcecoesPersonalizadas("Nenhum agendamento encontrado para a data informada.");

            return _mapper.Map<IEnumerable<Agendamento>,IEnumerable<AgendamentoViewModel>>(agendamentos);
        }
    }
}