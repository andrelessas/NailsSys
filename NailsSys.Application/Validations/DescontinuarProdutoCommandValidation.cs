using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.ProdutoCommands.DescontinuarProduto;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class DescontinuarProdutoCommandValidation:AbstractValidator<DescontinuarProdutoCommand>
    {
        public DescontinuarProdutoCommandValidation()
        {
            RuleFor(x=> x.Id)
                .NotNull().WithMessage(MensagensProduto.IdProdutoNaoInformadoParaBloqueioProduto)
                .NotEmpty().WithMessage(MensagensProduto.IdProdutoNaoInformadoParaBloqueioProduto)
                .GreaterThan(0).WithMessage(MensagensProduto.IdProdutoMaiorQueZero);
        }
    }
}