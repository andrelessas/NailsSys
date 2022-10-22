using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItens
{
    public class ObterItensAgendamentoQueriesHandler : IRequestHandler<ObterItensAgendamentoQueries, PaginationResult<ItemAgendamentoViewModel>>
    {
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;
        private readonly IMapper _mapper;

        public ObterItensAgendamentoQueriesHandler(IItemAgendamentoRepository itemAgendamentoRepository,
                                        IMapper mapper)
        {
            _itemAgendamentoRepository = itemAgendamentoRepository;
            _mapper = mapper;
        }
        public async Task<PaginationResult<ItemAgendamentoViewModel>> Handle(ObterItensAgendamentoQueries request, CancellationToken cancellationToken)
        {
            var itens = await _itemAgendamentoRepository.ObterItensAsync(request.IdAgendamento,request.Page);

            if(itens == null || itens.Data.Count() == 0)
                throw new ExcecoesPersonalizadas("Nenhum item encontrado.");
            
            return _mapper.Map<PaginationResult<ItemAgendamentoViewModel>>(itens);
        }
    }
}