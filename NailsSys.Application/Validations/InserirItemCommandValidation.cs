using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.ItemAgendamentoCommands.InserirItemCommand;

namespace NailsSys.Application.Validations
{
    public class InserirItemCommandValidation:AbstractValidator<InserirItemCommand>
    {
        public InserirItemCommandValidation()
        {
            RuleFor(i => i.IdProduto)
                .NotEmpty().WithMessage("Necessário informar o Id do Produto.")
                .NotNull().WithMessage("Necessário informar o Id do Produto.")
                .GreaterThan(0).WithMessage("Id Produto inválido, o Id Produto deve ser maior que 0.");

            RuleFor(i => i.IdAgendamento)
                .NotEmpty().WithMessage("Necessário informar o Id do Agendamento.")
                .NotNull().WithMessage("Necessário informar o Id do Agendamento.")
                .GreaterThan(0).WithMessage("Id Agendamento inválido, o Id Agendamento deve ser maior que 0.");

            RuleFor(i => i.Quantidade)
                .NotEmpty().WithMessage("Necessário informar a quantidade do produto.")
                .NotNull().WithMessage("Necessário informar a quantidade do produto.")
                .GreaterThan(0).WithMessage("Quantidade inválida, a quantidade deve ser maior que 0.");
                
        }
    }
}