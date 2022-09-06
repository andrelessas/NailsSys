using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.UsuarioCommands.InserirUsuario;

namespace NailsSys.Application.Validations
{
    public class InserirUsuarioValidation:AbstractValidator<InserirUsuarioCommand>
    {
        public InserirUsuarioValidation()
        {
            RuleFor(u => u.NomeCompleto)
                .NotNull()
                .NotEmpty()
                .WithMessage("Necessário informar o nome completo.")
                .MaximumLength(70)
                .MinimumLength(5)
                .WithMessage("O nome do usuário deve conter no mínimo 5 caracteres e no máximo 70 caracteres.");

            RuleFor(u => u.Login)
                .NotNull()
                .NotEmpty()
                .WithMessage("Necessário informar o login do usuário.")
                .MaximumLength(15)
                .MinimumLength(5)
                .WithMessage("O nome do usuário deve conter no mínimo 5 caracteres e no máximo 15 caracteres.");

            RuleFor(u => u.Senha)
                .NotNull()
                .NotEmpty()
                .WithMessage("Necessário informar a senha do usuário.")
                .Must(ValidarSenha)
                .WithMessage("Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caractere especial");
            
            RuleFor(u => u.Cargo)
                .NotNull()
                .NotEmpty()
                .WithMessage("Necessário informar o cargo do usuário.");
        }
        public bool ValidarSenha(string password)
        {
            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

            return regex.IsMatch(password);
        }
    }
}