using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.UsuarioCommands.InserirUsuario;
using NailsSys.Core.Enums;

namespace NailsSys.Application.Validations
{
    public class InserirUsuarioCommandValidation:AbstractValidator<InserirUsuarioCommand>
    {
        public InserirUsuarioCommandValidation()
        {
            RuleFor(u => u.NomeCompleto)
                .NotNull()
                .WithMessage("Necessário informar o nome completo.")
                .NotEmpty()
                .WithMessage("Necessário informar o nome completo.")
                .MaximumLength(70)
                .WithMessage("O nome do usuário deve conter no mínimo 5 caracteres e no máximo 70 caracteres.")
                .MinimumLength(5)
                .WithMessage("O nome do usuário deve conter no mínimo 5 caracteres e no máximo 70 caracteres.");

            RuleFor(u => u.Login)
                .NotNull()
                .WithMessage("Necessário informar o login do usuário.")
                .NotEmpty()
                .WithMessage("Necessário informar o login do usuário.")
                .MaximumLength(15)
                .WithMessage("O login do usuário deve conter no mínimo 5 caracteres e no máximo 15 caracteres.")
                .MinimumLength(5)
                .WithMessage("O login do usuário deve conter no mínimo 5 caracteres e no máximo 15 caracteres.");

            RuleFor(u => u.Senha)
                .NotNull()
                .WithMessage("Necessário informar a senha do usuário.")
                .NotEmpty()
                .WithMessage("Necessário informar a senha do usuário.")
                .Must(ValidarSenha)
                .WithMessage("Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caractere especial");
            
            RuleFor(u => u.Cargo)
                .NotNull()
                .WithMessage("Necessário informar o cargo do usuário.")
                .NotEmpty()
                .WithMessage("Necessário informar o cargo do usuário.")
                .Must(ValidarCargo)
                .WithMessage("Cargo do usuário inválido, o cargo deve ser Adminitrador, Gerente ou Atendente.");
        }
        public bool ValidarSenha(string password)
        {
            if (String.IsNullOrEmpty(password))
                return false;

            var regex = new Regex(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");

            return regex.IsMatch(password);
        }

        public bool ValidarCargo(Cargos cargo)
        {
            return Enum.IsDefined(typeof(Cargos),cargo);
        }
    }
}