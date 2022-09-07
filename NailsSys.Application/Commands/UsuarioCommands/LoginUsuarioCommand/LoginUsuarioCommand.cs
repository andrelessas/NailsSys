using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Commands.UsuarioCommands.LoginUsuarioCommand
{
    public class LoginUsuarioCommand:IRequest<LoginUsuarioViewModel>
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}