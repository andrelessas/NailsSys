using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.AgendamentoCommands.CancelamentoAgendamento;

namespace NailsSys.Application.Validations
{
    public class CancelarAgendamentoCommandValidation:AbstractValidator<CancelarAgendamentoCommand>
    {
        public CancelarAgendamentoCommandValidation()
        {
            RuleFor(x=> x.Id)
                .NotEmpty().WithMessage("Para cancelar o agendamento, é necessário informar o Id do Agendamento.")
                .GreaterThan(0).WithMessage("Para cancelar o agendamento, é necessário informar o Id do Agendamento.")
                .NotNull().WithMessage("Para cancelar o agendamento, é necessário informar o Id do Agendamento.");
        }
    }
}