using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.AtendimentoCommands.CancelarAtendimento
{
    public class CancelarAtendimentoCommandHandler : IRequestHandler<CancelarAtendimentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public CancelarAtendimentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(CancelarAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var atendimento = await _unitOfWorks.Atendimento.ObterPorIDAsync(request.Id);
            if(atendimento == null)
                throw new ExcecoesPersonalizadas("Atendimento não encontrado.");
            atendimento.CancelarAtendimento();
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}