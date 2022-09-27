using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.AgendamentoCommands.AlterarAgendamento
{
    public class AlterarAgendamentoCommandHandler : IRequestHandler<AlterarAgendamentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        public AlterarAgendamentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(AlterarAgendamentoCommand request, CancellationToken cancellationToken)
        {
            var agendamento = await _unitOfWorks.Agendamento.ObterPorIDAsync(request.Id);
            
            if(agendamento == null)
                throw new ExcecoesPersonalizadas("Agendamento não encontrado.");

            agendamento.AlterarAgendamento(request.IdCliente,request.DataAtendimento,request.InicioPrevisto,request.TerminoPrevisto);
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}