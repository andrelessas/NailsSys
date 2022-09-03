using FluentValidation;
using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;

namespace NailsSys.Application.Validations
{
    public class InserirProdutoCommandValidation:AbstractValidator<InserirProdutoCommand>
    {
        public InserirProdutoCommandValidation()
        {
            RuleFor(x=>x.Descricao)
                .NotNull()
                .NotEmpty()
                .WithMessage("Informe o nome do produto.")
                .MaximumLength(50)
                .WithMessage("Descrição do produto deve ter no máximo 50 caracteres.");
            
            RuleFor(x=>x.TipoProduto)
                .NotEmpty()
                .NotNull()
                .Must(ValidarTipoProduto)
                .MaximumLength(1)
                .WithMessage("Necessário informar o tipo do produto, se é S - Serviço ou P - Produto.");

            RuleFor(x=>x.Preco)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty()
                .WithMessage("Informe o preço de venda do produto.");
        }
        private bool ValidarTipoProduto(string tipoProduto)
            => tipoProduto == "S" || tipoProduto == "P";
    }
}