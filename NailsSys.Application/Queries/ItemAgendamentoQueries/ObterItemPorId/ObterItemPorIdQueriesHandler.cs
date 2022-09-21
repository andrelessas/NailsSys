using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItemPorId
{
    public class ObterItemPorIdQueriesHandler : IRequestHandler<ObterItemPorIdQueries, ItemAgendamentoViewModel>
    {
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;
        private readonly IMapper _mapper;

        public ObterItemPorIdQueriesHandler(IItemAgendamentoRepository itemAgendamentoRepository,
                                            IMapper mapper)
        {
            _itemAgendamentoRepository = itemAgendamentoRepository;
            _mapper = mapper;
        }
        public async Task<ItemAgendamentoViewModel> Handle(ObterItemPorIdQueries request, CancellationToken cancellationToken)
        {
            var item = await _itemAgendamentoRepository.ObterItemPorId(request.IdAgendamento);

            if (item == null)
                throw new ExcecoesPersonalizadas("Item não encontrado.");

            return _mapper.Map<ItemAgendamentoViewModel>(item);
        }
    }
}