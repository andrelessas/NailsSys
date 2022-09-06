using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Enums;

namespace NailsSys.Application.Commands.UsuarioCommands.InserirUsuario
{
    public class InserirUsuarioCommand:IRequest<Unit>
    {
        public string NomeCompleto { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public Cargos Cargo { get; set; }
    }
}