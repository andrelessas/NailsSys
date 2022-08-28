using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.ClienteCommands.AlterarCliente
{
    public class AlterarClienteCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string Telefone { get; set; }   
    }
}