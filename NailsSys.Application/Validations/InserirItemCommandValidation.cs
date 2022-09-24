using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.ItemAgendamentoCommands.InserirItemCommand;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class InserirItemCommandValidation:AbstractValidator<InserirItemCommand>
    {
        public InserirItemCommandValidation()
        {
            RuleFor(i => i.IdProduto)
                .NotEmpty().WithMessage(MensagensItensAgendamento.IdProdutoNullVazio)
                .NotNull().WithMessage(MensagensItensAgendamento.IdProdutoNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAgendamento.IdProdutoMaiorQueZero);

            RuleFor(i => i.IdAgendamento)
                .NotEmpty().WithMessage(MensagensItensAgendamento.IdAgendamentoNullVazio)
                .NotNull().WithMessage(MensagensItensAgendamento.IdAgendamentoNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAgendamento.IdAgendamentoMaiorQueZero);

            RuleFor(i => i.Quantidade)
                .NotEmpty().WithMessage(MensagensItensAgendamento.QuantidadeNullVazio)
                .NotNull().WithMessage(MensagensItensAgendamento.QuantidadeNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAgendamento.QuantidadeMaiorQueZero);
                
        }
    }
}