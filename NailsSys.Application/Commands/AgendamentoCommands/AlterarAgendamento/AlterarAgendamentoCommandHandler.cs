using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.AgendamentoCommands.AlterarAgendamento
{
    public class AlterarAgendamentoCommandHandler : IRequestHandler<AlterarAgendamentoCommand, Unit>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;

        public AlterarAgendamentoCommandHandler(IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }
        public async Task<Unit> Handle(AlterarAgendamentoCommand request, CancellationToken cancellationToken)
        {
            var agendamento = await _agendamentoRepository.ObterPorIDAsync(request.Id);
            agendamento.AlterarAgendamento(request.IdCliente,request.DataAtendimento,request.InicioPrevisto,request.TerminoPrevisto);
            await _agendamentoRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}