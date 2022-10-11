using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.AtendimentoCommands.CancelarAtendimento
{
    public class CancelarAtendimentoCommand:IRequest<Unit>
    {
        public CancelarAtendimentoCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}