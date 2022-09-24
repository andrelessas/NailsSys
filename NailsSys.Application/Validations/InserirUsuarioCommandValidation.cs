using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.UsuarioCommands.InserirUsuario;
using NailsSys.Core.Enums;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class InserirUsuarioCommandValidation:AbstractValidator<InserirUsuarioCommand>
    {
        public InserirUsuarioCommandValidation()
        {
            RuleFor(u => u.NomeCompleto)
                .NotNull().WithMessage(MensagensUsuario.NomeCompletoNullVazio)
                .NotEmpty().WithMessage(MensagensUsuario.NomeCompletoNullVazio)
                .MaximumLength(70).WithMessage(MensagensUsuario.NomeCompletoCurtoOuLongo)
                .MinimumLength(5).WithMessage(MensagensUsuario.NomeCompletoCurtoOuLongo);

            RuleFor(u => u.Login)
                .NotNull().WithMessage(MensagensUsuario.LoginNullVazio)
                .NotEmpty().WithMessage(MensagensUsuario.LoginNullVazio)
                .MaximumLength(15).WithMessage(MensagensUsuario.LoginCurtoOuLongo)
                .MinimumLength(5).WithMessage(MensagensUsuario.LoginCurtoOuLongo);

            RuleFor(u => u.Senha)
                .NotNull().WithMessage(MensagensUsuario.SenhaNullVazio)
                .NotEmpty().WithMessage(MensagensUsuario.SenhaNullVazio)
                .Must(ValidarSenha).WithMessage(MensagensUsuario.SenhaFraca);
            
            RuleFor(u => u.Cargo)
                .NotNull().WithMessage(MensagensUsuario.CargoNullVazio)
                .NotEmpty().WithMessage(MensagensUsuario.CargoNullVazio)
                .Must(ValidarCargo).WithMessage(MensagensUsuario.CargoInvalido);
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