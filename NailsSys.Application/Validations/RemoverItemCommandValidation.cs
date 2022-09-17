using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.ItemAgendamentoCommands.RemoverItem;

namespace NailsSys.Application.Validations
{
    public class RemoverItemCommandValidation : AbstractValidator<RemoverItemCommand>
    {
        public RemoverItemCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Para remover o item do agendamento, é necessário informar o Item.")
                .NotEmpty().WithMessage("Para remover o item do agendamento, é necessário informar o Item.")
                .GreaterThan(0).WithMessage("Para remover o item do agendamento, é necessário informar o Item.");
        }
    }
}