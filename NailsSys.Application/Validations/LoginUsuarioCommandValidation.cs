using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.UsuarioCommands.LoginUsuarioCommand;

namespace NailsSys.Application.Validations
{
    public class LoginUsuarioCommandValidation:AbstractValidator<LoginUsuarioCommand>
    {
        public LoginUsuarioCommandValidation()
        {
            RuleFor(i => i.Id)
                .Must(ValidarId).WithMessage("Necessário informar o Id ou Login do usuário para acessar o sistema.");

            RuleFor(u => u.Usuario)
                .Must(ValidarLogin).WithMessage("Necessário informar o Id ou Login do usuário para acessar o sistema.");
            
            RuleFor(s => s.Senha)
                .NotNull().WithMessage("Necessário informar a senha de acesso ao sistema.")
                .NotEmpty().WithMessage("Necessário informar a senha de acesso ao sistema.");
        }

        public bool ValidarLogin(string login)
        {
            return ValidarIDLogin(login: login);
        }
        public bool ValidarId(int id)
        {
            return ValidarIDLogin(id);
        }
        private bool ValidarIDLogin(int id = 0, string login = "")
        {
            return id > 0 || !String.IsNullOrEmpty(login);
        }
    }
}