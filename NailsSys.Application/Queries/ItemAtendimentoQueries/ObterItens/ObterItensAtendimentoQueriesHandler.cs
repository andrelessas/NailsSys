using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItens;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.ItemAtendimentoQueries.ObterItens
{
    public class ObterItensAtendimentoQueriesHandler : IRequestHandler<ObterItensAtendimentoQueries,PaginationResult<ItemAtendimentoViewModel>>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;

        public ObterItensAtendimentoQueriesHandler(IUnitOfWorks unitOfWorks,IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
        }
        public async Task<PaginationResult<ItemAtendimentoViewModel>> Handle(ObterItensAtendimentoQueries request, CancellationToken cancellationToken)
        {
            var itens = await _unitOfWorks.ItemAtendimento.ObterAsync(request.Page,x=>x.IdAtendimento == request.IdAtendimento);

            if(!itens.Data.Any())
                throw new ExcecoesPersonalizadas("Nenhum item encontrado.");
            
            return _mapper.Map<PaginationResult<ItemAtendimentoViewModel>>(itens);
        }
    }
}