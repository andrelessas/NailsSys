using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItens
{
    public class ObterItensQueriesHandler : IRequestHandler<ObterItensQueries, IEnumerable<ItemAgendamentoViewModel>>
    {
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;

        public ObterItensQueriesHandler(IItemAgendamentoRepository itemAgendamentoRepository)
        {
            _itemAgendamentoRepository = itemAgendamentoRepository;
        }
        public async Task<IEnumerable<ItemAgendamentoViewModel>> Handle(ObterItensQueries request, CancellationToken cancellationToken)
        {
            var itens = await _itemAgendamentoRepository.ObterItensAsync(request.IdAgendamento);
            return itens.Select(i => new ItemAgendamentoViewModel(i.DescricaoProduto,i.Quantidade,i.PrecoInicial,i.Id))
                        .ToList();
        }
    }
}