using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItemPorId
{
    public class ObterItemPorIdQueriesHandler : IRequestHandler<ObterItemPorIdQueries, ItemAgendamentoViewModel>
    {
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;

        public ObterItemPorIdQueriesHandler(IItemAgendamentoRepository itemAgendamentoRepository)
        {
            _itemAgendamentoRepository = itemAgendamentoRepository;
        }
        public async Task<ItemAgendamentoViewModel> Handle(ObterItemPorIdQueries request, CancellationToken cancellationToken)
        {
            var item = await _itemAgendamentoRepository.ObterItemPorId(request.IdAgendamento);
            return new ItemAgendamentoViewModel(item.DescricaoProduto,item.Quantidade,item.PrecoInicial,item.Id);
        }
    }
}