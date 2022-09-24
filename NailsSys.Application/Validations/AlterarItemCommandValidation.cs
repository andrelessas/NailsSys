using FluentValidation;
using NailsSys.Application.Commands.ItemAgendamentoCommands.AlterarItem;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class AlterarItemCommandValidation:AbstractValidator<AlterarItemCommand>
    {
        public AlterarItemCommandValidation()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage(MensagensItensAgendamento.ItemNullVazio)
                .NotNull().WithMessage(MensagensItensAgendamento.ItemNullVazio)      
                .GreaterThan(0).WithMessage(MensagensItensAgendamento.ItemMaiorQueZero);
                
            RuleFor(a => a.Quantidade)
                .NotEmpty().WithMessage(MensagensItensAgendamento.QuantidadeNullVazio)
                .NotNull().WithMessage(MensagensItensAgendamento.QuantidadeNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAgendamento.QuantidadeMaiorQueZero);
        }
    }
}