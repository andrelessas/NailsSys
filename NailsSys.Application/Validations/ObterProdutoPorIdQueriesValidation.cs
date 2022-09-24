using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Queries.ProdutoQueries.ObterProdutoPorId;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class ObterProdutoPorIdQueriesValidation:AbstractValidator<ObterProdutoPorIdQueries>
    {
        public ObterProdutoPorIdQueriesValidation()
        {
            RuleFor(x=>x.Id)
                .NotNull().WithMessage(MensagensProduto.IdProdutoNullVazio)
                .NotEmpty().WithMessage(MensagensProduto.IdProdutoNullVazio)
                .GreaterThan(0).WithMessage(MensagensProduto.IdProdutoMaiorQueZero);
        }
    }
}