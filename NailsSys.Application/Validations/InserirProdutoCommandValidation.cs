using FluentValidation;
using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class InserirProdutoCommandValidation:AbstractValidator<InserirProdutoCommand>
    {
        public InserirProdutoCommandValidation()
        {
            RuleFor(x=>x.Descricao)
                .NotNull().WithMessage(MensagensProduto.DescricaoNullVazio)
                .NotEmpty().WithMessage(MensagensProduto.DescricaoNullVazio)
                .MaximumLength(50).WithMessage(MensagensProduto.LimiteTamanhoDescricao);
            
            RuleFor(x=>x.TipoProduto)
                .NotEmpty().WithMessage(MensagensProduto.TipoProdutoNullVazio)
                .NotNull().WithMessage(MensagensProduto.TipoProdutoNullVazio)
                .Must(ValidarTipoProduto).WithMessage(MensagensProduto.ValidarTipoProduto);

            RuleFor(x=>x.Preco)
                .GreaterThan(0).WithMessage(MensagensProduto.PrecoMaiorQueZero)
                .NotNull().WithMessage(MensagensProduto.PrecoNullVazio)
                .NotEmpty().WithMessage(MensagensProduto.PrecoNullVazio);
        }
        private bool ValidarTipoProduto(string tipoProduto)
            => tipoProduto == "S" || tipoProduto == "P";
    }
}