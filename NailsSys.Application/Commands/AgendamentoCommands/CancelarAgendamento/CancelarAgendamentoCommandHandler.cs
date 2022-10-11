using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.AgendamentoCommands.CancelarAgendamento
{
    public class CancelarAgendamentoCommandHandler : IRequestHandler<CancelarAgendamentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public CancelarAgendamentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(CancelarAgendamentoCommand request, CancellationToken cancellationToken)
        {
            var agendamento = await _unitOfWorks.Agendamento.ObterPorIDAsync(request.Id);

            if(agendamento == null)
                throw new ExcecoesPersonalizadas("Nenhum agendamento encontrado.");

            agendamento.CancelarAgendamento();
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}