using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.AgendamentoCommands.NovoAgendamento
{
    public class NovoAgendamentoCommand:IRequest<Unit>
    {
        public int idCliente { get; set; }
        public DateTime DataAtendimento { get; set; }
        public DateTime InicioPrevisto { get; set; }
        public DateTime TerminoPrevisto { get; set; }
    }
}