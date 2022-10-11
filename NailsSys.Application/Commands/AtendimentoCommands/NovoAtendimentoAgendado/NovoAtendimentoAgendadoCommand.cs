using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.AtendimentoCommands.NovoAtendimentoAgendado
{
    public class NovoAtendimentoAgendadoCommand:IRequest<Unit>
    {
        public int IdAgendamento { get; set; }
        public int IdFormaPagamento { get; set; }
        public DateTime? InicioAtendimento { get; set; }
        public DateTime? TerminoAtendimento { get; set; }
    }
}