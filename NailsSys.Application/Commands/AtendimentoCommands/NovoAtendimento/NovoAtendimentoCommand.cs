using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.AtendimentoCommands.NovoAtendimento
{
    public class NovoAtendimentoCommand:IRequest<Unit>
    {
        public int IdAgendamento { get; set; }   
        public int IdFormaPagamento { get; set; }     
    }
}