using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Queries.ClienteQueries.ObterClientes;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class ObterClientesQueriesValidation:AbstractValidator<ObterClientesQueries>
    {
        public ObterClientesQueriesValidation()
        {
            RuleFor(x => x.Page)
                .NotNull().WithMessage(MensagensPaginacao.PageNullVazio)
                .NotEmpty().WithMessage(MensagensPaginacao.PageNullVazio)
                .GreaterThan(0).WithMessage(MensagensPaginacao.PageMaiorQueZero);
        }
    }
}