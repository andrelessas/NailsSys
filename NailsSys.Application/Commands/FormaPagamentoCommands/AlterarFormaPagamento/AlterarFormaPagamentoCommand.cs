using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.FormaPagamentoCommands.AlterarFormaPagamento
{
    public class AlterarFormaPagamentoCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string AVistaAPrazo { get; set; }
    }
}