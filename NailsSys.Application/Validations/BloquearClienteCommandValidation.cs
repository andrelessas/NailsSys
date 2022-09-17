using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.ClienteCommands.BloquearCliente;

namespace NailsSys.Application.Validations
{
    public class BloquearClienteCommandValidation:AbstractValidator<BloquearClienteCommand>
    {
        public BloquearClienteCommandValidation()
        {
            RuleFor(x=>x.Id)
                .NotNull().WithMessage("Para bloquear o cliente, é necessário informar o Id do Cliente válido.")
                .NotEmpty().WithMessage("Para bloquear o cliente, é necessário informar o Id do Cliente válido.")
                .GreaterThan(0).WithMessage("Para bloquear o cliente, é necessário informar o Id do Cliente válido.");
        }
    }
}