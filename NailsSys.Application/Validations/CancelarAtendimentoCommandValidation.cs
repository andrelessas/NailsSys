using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.AtendimentoCommands.CancelarAtendimento;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class CancelarAtendimentoCommandValidation:AbstractValidator<CancelarAtendimentoCommand>
    {
        public CancelarAtendimentoCommandValidation()
        {
            RuleFor(x=>x.Id)
                .NotEmpty().WithMessage(MensagensAtendimento.IdAtendimentoNullVazioCancelamento)
                .NotNull().WithMessage(MensagensAtendimento.IdAtendimentoNullVazioCancelamento)
                .GreaterThan(0).WithMessage(MensagensAtendimento.IdAtendimentoMaiorQueZero);
        }
    }
}