using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.ClienteCommands.BloquearCliente
{
    public class BloquearClienteCommand:IRequest<Unit>
    {
        public int Id { get; set; }
    }
}