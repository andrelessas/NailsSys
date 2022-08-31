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
            var lstItens = new List<ItemAgendamentoViewModel>();
            var lstAgendamentos = new List<AgendamentoViewModel>();

            var agendamentos = await _agendamentoRepository.ObterAgendamentosPorDataAsync(request.Data);        
            
            lstAgendamentos = agendamentos.ToList().ConvertAll(a => new AgendamentoViewModel(a.DataAtendimento,
                                                                                             a.InicioPrevisto,
                                                                                             a.TerminoPrevisto,
                                                                                             a.Cliente.NomeCliente,
                                                                                             a.Id));
                                                                                             
            foreach (var agendamento in lstAgendamentos)
            {
                var itensDTO = await _itemAgendamentoRepository.ObterItensAsync(agendamento.Id);                
                agendamento.Itens = itensDTO.ToList().ConvertAll(i => new ItemAgendamentoViewModel(i.DescricaoProduto,i.Quantidade,i.PrecoInicial,i.Id));                
            }

            return lstAgendamentos;
        }
    }
}