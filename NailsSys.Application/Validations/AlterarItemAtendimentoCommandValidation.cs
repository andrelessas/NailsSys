using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.ItemAtendimentoCommands.AlterarItem;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class AlterarItemAtendimentoCommandValidation:AbstractValidator<AlterarItemAtendimentoCommand>
    {
        public AlterarItemAtendimentoCommandValidation()
        {
            RuleFor(x=>x.Item)
                .NotNull().WithMessage(MensagensItensAtendimento.ItemNullVazio)
                .NotEmpty().WithMessage(MensagensItensAtendimento.ItemNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAtendimento.ItemMaiorQueZero);

            RuleFor(x=>x.Quantidade)
                .NotNull().WithMessage(MensagensItensAtendimento.QuantidadeNullVazio)
                .NotEmpty().WithMessage(MensagensItensAtendimento.QuantidadeNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAtendimento.QuantidadeMaiorQueZero);

            RuleFor(x=>x.IdProduto)
                .NotNull().WithMessage(MensagensItensAtendimento.IdProdutoNullVazio)
                .NotEmpty().WithMessage(MensagensItensAtendimento.IdProdutoNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAtendimento.IdProdutoMaiorQueZero);

            RuleFor(x=>x.IdAtendimento)
                .NotNull().WithMessage(MensagensItensAtendimento.IdAtendimentoNullVazio)
                .NotEmpty().WithMessage(MensagensItensAtendimento.IdAtendimentoNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAtendimento.IdAtendimentoMaiorQueZero);
        }
    }
}