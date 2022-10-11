using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.AtendimentoCommands.NovoAtendimentoAgendado;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class NovoAtendimentoAgendadoCommandValidation:AbstractValidator<NovoAtendimentoAgendadoCommand>
    {
        public NovoAtendimentoAgendadoCommandValidation()
        {
            RuleFor(x=>x.IdAgendamento)
                .NotEmpty().WithMessage(MensagensAgendamento.IdAgendamentoNaoInformado)
                .NotNull().WithMessage(MensagensAgendamento.IdAgendamentoNaoInformado)
                .GreaterThan(0).WithMessage(MensagensAgendamento.IdAgendamentoMaiorQueZero);

            RuleFor(x=>x.IdFormaPagamento)
                .NotEmpty().WithMessage(MensagensAtendimento.IdFormaPgtoNullVazio)
                .NotNull().WithMessage(MensagensAtendimento.IdFormaPgtoNullVazio)
                .GreaterThan(0).WithMessage(MensagensAtendimento.IdFormaPgtoMaiorQueZero);
        }
    }
}