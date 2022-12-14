using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.RemoverItem
{
    public class RemoverItemAgendamentoCommand:IRequest<Unit>
    {
        public RemoverItemAgendamentoCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}