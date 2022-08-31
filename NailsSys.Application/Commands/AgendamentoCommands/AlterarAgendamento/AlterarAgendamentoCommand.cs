using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.AgendamentoCommands.AlterarAgendamento
{
    public class AlterarAgendamentoCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataAtendimento { get; set; }
        public DateTime InicioPrevisto { get; set; }
        public DateTime TerminoPrevisto { get; set; }
    }
}