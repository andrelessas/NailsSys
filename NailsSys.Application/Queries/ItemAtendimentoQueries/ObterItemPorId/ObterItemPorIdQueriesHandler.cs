using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.ItemAtendimentoQueries.ObterItemPorId
{
    public class ObterItemPorIdQueriesHandler : IRequestHandler<ObterItemPorIdQueries, ItemAtendimentoViewModel>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;

        public ObterItemPorIdQueriesHandler(IUnitOfWorks unitOfWorks,IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
        }
        public async Task<ItemAtendimentoViewModel> Handle(ObterItemPorIdQueries request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWorks.ItemAtendimento.ObterItemAtendimento(request.Item,request.IdAtendimento);

            if(item == null)
                throw new ExcecoesPersonalizadas("Item não encontrado.");

            return _mapper.Map<ItemAtendimentoViewModel>(item);
        }
    }
}