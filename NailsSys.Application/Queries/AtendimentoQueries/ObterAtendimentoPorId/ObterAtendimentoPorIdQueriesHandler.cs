using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.AtendimentoQueries.ObterAtendimentoPorId
{
    public class ObterAtendimentoPorIdQueriesHandler : IRequestHandler<ObterAtendimentoPorIDQueries, AtendimentoViewModel>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;

        public ObterAtendimentoPorIdQueriesHandler(IUnitOfWorks unitOfWorks, IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
        }
        public async Task<AtendimentoViewModel> Handle(ObterAtendimentoPorIDQueries request, CancellationToken cancellationToken)
        {
            var atendimento = await _unitOfWorks.Atendimento.ObterPorId(request.Id);
            if(atendimento == null)
                throw new ExcecoesPersonalizadas("Atendimento não encontrado.");

            return _mapper.Map<AtendimentoViewModel>(atendimento);
        }
    }
}