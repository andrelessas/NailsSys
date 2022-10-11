using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.AlterarItem
{
    public class AlterarItemAgendamentoCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
    }
}