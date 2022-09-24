using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.UsuarioCommands.LoginUsuarioCommand;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class LoginUsuarioCommandValidation:AbstractValidator<LoginUsuarioCommand>
    {
        public LoginUsuarioCommandValidation()
        {
            RuleFor(i => i.Id)
                .Must(ValidarId).WithMessage(MensagensLogin.LoginNullVazio);

            RuleFor(u => u.Usuario)
                .Must(ValidarLogin).WithMessage(MensagensLogin.LoginNullVazio);
            
            RuleFor(s => s.Senha)
                .NotNull().WithMessage(MensagensLogin.SenhaNullVazio)
                .NotEmpty().WithMessage(MensagensLogin.SenhaNullVazio);
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