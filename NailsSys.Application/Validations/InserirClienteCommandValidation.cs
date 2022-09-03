using System.Text.RegularExpressions;
using FluentValidation;
using NailsSys.Application.Commands.ClienteCommands.InserirCliente;

namespace NailsSys.Application.Validations
{
    public class InserirClienteCommandValidation:AbstractValidator<InserirClienteCommand>
    {
        public InserirClienteCommandValidation()
        {
            RuleFor(x=>x.NomeCliente)
                .NotNull()
                .NotEmpty()
                .WithMessage("Necessário informar o nome do cliente")
                .MaximumLength(50)
                .WithMessage("O nome do cliente deve ter no máximo 50 caracteres");

            RuleFor(x=>x.Telefone)            
                .NotNull()
                .NotEmpty()
                .Must(ValidarTelefone)
                .WithMessage("Informe um telefone válido.");
        }

        public bool ValidarTelefone(string phone)
        {
            var regex = new Regex(@"^1\d\d(\d\d)?$|^0800 ?\d{3} ?\d{4}$|^(\(0?([1-9a-zA-Z][0-9a-zA-Z])?[1-9]\d\) ?|0?([1-9a-zA-Z][0-9a-zA-Z])?[1-9]\d[ .-]?)?(9|9[ .-])?[2-9]\d{3}[ .-]?\d{4}$");
            return regex.IsMatch(phone);
        }
    }
}