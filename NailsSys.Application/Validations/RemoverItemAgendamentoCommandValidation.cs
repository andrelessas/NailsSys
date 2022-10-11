using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.ItemAgendamentoCommands.RemoverItem;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class RemoverItemAgendamentoCommandValidation : AbstractValidator<RemoverItemAgendamentoCommand>
    {
        public RemoverItemAgendamentoCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage(MensagensItensAgendamento.ItemNullVazio)
                .NotEmpty().WithMessage(MensagensItensAgendamento.ItemNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAgendamento.ItemMaiorQueZero);
        }
    }
}