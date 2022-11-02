using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.AtendimentoQueries.ObterAtendimentoRealizadosHoje
{
    public class ObterAtendimentosRealizadosHojeQueriesHandler : IRequestHandler<ObterAtendimentosRealizadosHojeQueries, IEnumerable<AtendimentoViewModel>>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private IMapper _mapper;

        public ObterAtendimentosRealizadosHojeQueriesHandler(IUnitOfWorks unitOfWorks, IMapper mapper)        
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AtendimentoViewModel>> Handle(ObterAtendimentosRealizadosHojeQueries request, CancellationToken cancellationToken)
        {
            var atendimentos = await _unitOfWorks.Atendimento.ObterAtendimentosRealizadosHoje();

            if(!atendimentos.Any())
                throw new ExcecoesPersonalizadas("Nenhum atendimento foi realizado hoje.");

            return _mapper.Map<IEnumerable<Atendimento>,IEnumerable<AtendimentoViewModel>>(atendimentos);
        }
    }
}