using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.ItemAtendimentoCommands.RemoverItem
{
    public class RemoverItemAtendimentoCommand:IRequest<Unit>
    {
        public RemoverItemAtendimentoCommand(int idAtendimento, int item)
        {
            IdAtendimento = idAtendimento;
            Item = item;
        }

        public int IdAtendimento { get; set; }
        public int Item { get; set; }
    }
}