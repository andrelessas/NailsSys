using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.ProdutoCommands.DescontinuarProduto;

namespace NailsSys.Application.Validations
{
    public class DescontinuarProdutoCommandValidation:AbstractValidator<DescontinuarProdutoCommand>
    {
        public DescontinuarProdutoCommandValidation()
        {
            RuleFor(x=> x.Id)
                .NotNull().WithMessage("Necessário informar o Id do produto que será descontinuado.")
                .NotEmpty().WithMessage("Necessário informar o Id do produto que será descontinuado.")
                .GreaterThan(0).WithMessage("O Id do produto deve ser maior que 0.");
        }
    }
}