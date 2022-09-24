using System.Text.RegularExpressions;
using FluentValidation;
using NailsSys.Application.Commands.ClienteCommands.InserirCliente;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class InserirClienteCommandValidation:AbstractValidator<InserirClienteCommand>
    {
        public InserirClienteCommandValidation()
        {
            RuleFor(x=>x.NomeCliente)
                .NotNull().WithMessage(MensagensCliente.NomeClienteNullVazio)
                .NotEmpty().WithMessage(MensagensCliente.NomeClienteNullVazio)
                .MaximumLength(50).WithMessage(MensagensCliente.NomeClienteQuantidadeCaracteresSuperiorAoLimite);

            RuleFor(x=>x.Telefone)            
                .NotNull().WithMessage(MensagensCliente.TelefoneInvalido)
                .NotEmpty().WithMessage(MensagensCliente.TelefoneInvalido)
                .Must(ValidarTelefone).WithMessage(MensagensCliente.TelefoneInvalido);
        }

        public bool ValidarTelefone(string phone)
        {
            if(phone == null)
                return false;

            var regex = new Regex(@"^1\d\d(\d\d)?$|^0800 ?\d{3} ?\d{4}$|^(\(0?([1-9a-zA-Z][0-9a-zA-Z])?[1-9]\d\) ?|0?([1-9a-zA-Z][0-9a-zA-Z])?[1-9]\d[ .-]?)?(9|9[ .-])?[2-9]\d{3}[ .-]?\d{4}$");
            return regex.IsMatch(phone);
        }
    }
}