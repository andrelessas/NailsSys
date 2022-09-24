using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.ClienteCommands.BloquearCliente;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class BloquearClienteCommandValidation:AbstractValidator<BloquearClienteCommand>
    {
        public BloquearClienteCommandValidation()
        {
            RuleFor(x=>x.Id)
                .NotNull().WithMessage(MensagensCliente.IdClienteNaoInformadoAoBloquearCLiente)
                .NotEmpty().WithMessage(MensagensCliente.IdClienteNaoInformadoAoBloquearCLiente)
                .GreaterThan(0).WithMessage(MensagensCliente.IdClienteNaoInformadoAoBloquearCLiente);
        }
    }
}