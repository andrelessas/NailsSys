using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.AgendamentoCommands.CancelamentoAgendamento
{
    public class CancelarAgendamentoCommandHandler : IRequestHandler<CancelarAgendamentoCommand, Unit>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;

        public CancelarAgendamentoCommandHandler(IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }
        public async Task<Unit> Handle(CancelarAgendamentoCommand request, CancellationToken cancellationToken)
        {
            var agendamento = await _agendamentoRepository.ObterPorIDAsync(request.Id);
            agendamento.CancelarAgendamento();
            await _agendamentoRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}