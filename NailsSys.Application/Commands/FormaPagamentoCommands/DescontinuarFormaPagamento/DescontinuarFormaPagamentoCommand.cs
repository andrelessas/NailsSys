using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.FormaPagamentoCommands.DescontinuarFormaPagamento
{
    public class DescontinuarFormaPagamentoCommand:IRequest<Unit>
    {
        public DescontinuarFormaPagamentoCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}