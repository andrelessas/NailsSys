using FluentValidation;
using NailsSys.Application.Commands.ItemAgendamentoCommands.AlterarItem;

namespace NailsSys.Application.Validations
{
    public class AlterarItemCommandValidation:AbstractValidator<AlterarItemCommand>
    {
        public AlterarItemCommandValidation()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("Necessário informar o Item.")
                .NotNull().WithMessage("Necessário informar o Item.")                
                .GreaterThan(0).WithMessage("O Item deve ser maior que 0.");
                
            RuleFor(a => a.Quantidade)
                .NotEmpty().WithMessage("Necessário informar a quantidade do item.")
                .NotNull().WithMessage("Necessário informar a quantidade do item.")                
                .GreaterThan(0).WithMessage("A Quantidade deve ser maior que 0.");
        }
    }
}