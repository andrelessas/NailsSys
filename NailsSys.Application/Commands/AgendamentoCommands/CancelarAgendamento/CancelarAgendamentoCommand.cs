using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.AgendamentoCommands.CancelarAgendamento
{
    public class CancelarAgendamentoCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}