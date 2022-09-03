using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.ClienteCommands.BloquearCliente
{
    public class BloquearClienteCommand:IRequest<Unit>
    {
        public BloquearClienteCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}