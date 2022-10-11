using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.ItemAtendimentoCommands.RemoverItem;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class RemoverItemAtendimentoCommandValidation:AbstractValidator<RemoverItemAtendimentoCommand>
    {
        public RemoverItemAtendimentoCommandValidation()
        {
            RuleFor(x=>x.IdAtendimento)
                .NotNull().WithMessage(MensagensItensAtendimento.IdAtendimentoNullVazio)
                .NotEmpty().WithMessage(MensagensItensAtendimento.IdAtendimentoNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAtendimento.IdAtendimentoMaiorQueZero);

            RuleFor(x=>x.Item)
                .NotNull().WithMessage(MensagensItensAtendimento.ItemNullVazio)
                .NotEmpty().WithMessage(MensagensItensAtendimento.ItemNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAtendimento.ItemMaiorQueZero);
        }
    }
}