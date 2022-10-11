using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.AtendimentoCommands.AlterarAtendimento
{
    public class AlterarAtendimentoCommand:IRequest<Unit>
    {
        public int IdAtendimento { get; set; }
        public int IdFormaPagamento { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataAtendimento { get; set; }
        public DateTime InicioAtendimento { get; set; }
        public DateTime TerminoAtendimento { get; set; }        
    }
}