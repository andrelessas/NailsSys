using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.AgendamentoCommands.CancelamentoAgendamento;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class CancelarAgendamentoCommandValidation:AbstractValidator<CancelarAgendamentoCommand>
    {
        public CancelarAgendamentoCommandValidation()
        {
            RuleFor(x=> x.Id)
                .NotEmpty().WithMessage(MensagensAgendamento.IdAgendamentoNaoInformadoCancelarAgendamento)
                .GreaterThan(0).WithMessage(MensagensAgendamento.IdAgendamentoNaoInformadoCancelarAgendamento)
                .NotNull().WithMessage(MensagensAgendamento.IdAgendamentoNaoInformadoCancelarAgendamento);
        }
    }
}