using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

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
            return _mapper.Map<IEnumerable<AgendamentoViewModel>>(await _agendamentoRepository.ObterAgendamentosPorDataAsync(request.Data));
        }
    }
}