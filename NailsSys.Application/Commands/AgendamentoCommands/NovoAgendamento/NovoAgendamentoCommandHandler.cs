using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.AgendamentoCommands.NovoAgendamento
{
    public class NovoAgendamentoCommandHandler : IRequestHandler<NovoAgendamentoCommand, Unit>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;

        public NovoAgendamentoCommandHandler(IAgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }
        public async Task<Unit> Handle(NovoAgendamentoCommand request, CancellationToken cancellationToken)
        {
            _agendamentoRepository.InserirAsync(new Agendamento(request.idCliente,
                                                                request.DataAtendimento,
                                                                request.InicioPrevisto,
                                                                request.TerminoPrevisto));
            await _agendamentoRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}