using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Queries.ProdutoQueries.ObterProdutoPorId;

namespace NailsSys.Application.Validations
{
    public class ObterProdutoPorIdQueriesValidation:AbstractValidator<ObterProdutoPorIdQueries>
    {
        public ObterProdutoPorIdQueriesValidation()
        {
            RuleFor(x=>x.Id)
                .NotNull().WithMessage("Necessário informar o Id do produto.")
                .NotEmpty().WithMessage("Necessário informar o Id do produto.")
                .GreaterThan(0).WithMessage("O Id do produto deve ser maior que 0.");
        }
    }
}