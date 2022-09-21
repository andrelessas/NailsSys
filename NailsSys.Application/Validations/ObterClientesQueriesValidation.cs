using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Queries.ClienteQueries.ObterClientes;

namespace NailsSys.Application.Validations
{
    public class ObterClientesQueriesValidation:AbstractValidator<ObterClientesQueries>
    {
        public ObterClientesQueriesValidation()
        {
            RuleFor(x => x.Page)
                .NotNull().WithMessage("Necessário informar a quantidade de páginas para que seja realizada a paginação dos dados.")
                .NotEmpty().WithMessage("Necessário informar a quantidade de páginas para que seja realizada a paginação dos dados.")
                .GreaterThan(0).WithMessage("Necessário informar a quantidade de páginas para que seja realizada a paginação dos dados.");
        }
    }
}