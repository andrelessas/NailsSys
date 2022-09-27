using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.FormaPagamentoCommands.InserirFormaPagamento
{
    public class InserirFormaPagamentoCommand:IRequest<Unit>
    {
        public string Descricao { get; set; }   
        public string AVistaAPrazo { get; set; }
    }
}