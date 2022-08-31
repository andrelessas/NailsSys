using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorData
{
    public class ObterAgendamentosPorDataQueriesHandler : IRequestHandler<ObterAgendamentosPorDataQueries, IEnumerable<AgendamentoViewModel>>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;

        public ObterAgendamentosPorDataQueriesHandler(IAgendamentoRepository agendamentoRepository,IItemAgendamentoRepository itemAgendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _itemAgendamentoRepository = itemAgendamentoRepository;
        }
        public async Task<IEnumerable<AgendamentoViewModel>> Handle(ObterAgendamentosPorDataQueries request, CancellationToken cancellationToken)
        {
            var agendamentos = await _agendamentoRepository.ObterAgendamentosPorDataAsync(request.Data);

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