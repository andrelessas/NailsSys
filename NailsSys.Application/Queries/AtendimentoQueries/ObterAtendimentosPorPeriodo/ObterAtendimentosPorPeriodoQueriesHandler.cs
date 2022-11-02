using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.AtendimentoQueries.ObterAtendimentosPorPeriodo
{
    public class ObterAtendimentosPorPeriodoQueriesHandler : IRequestHandler<ObterAtendimentosPorPeriodoQueries, IEnumerable<AtendimentoViewModel>>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IMapper _mapper;

        public ObterAtendimentosPorPeriodoQueriesHandler(IUnitOfWorks unitOfWorks,IMapper mapper)
        {
            _unitOfWorks = unitOfWorks;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AtendimentoViewModel>> Handle(ObterAtendimentosPorPeriodoQueries request, CancellationToken cancellationToken)
        {
            var atendimentos = await _unitOfWorks.Atendimento.ObterAtendimentosPorPeriodo(request.DataInicial,request.DataFinal);
            if(!atendimentos.Any())
                throw new ExcecoesPersonalizadas("Nenhum atendimento encontrado.");

            return _mapper.Map<IEnumerable<Atendimento>,IEnumerable<AtendimentoViewModel>>(atendimentos);
        }
    }
}