using MediatR;

namespace NailsSys.Application.Commands.ProdutoCommands.InserirProduto
{
    public class InserirProdutoCommand:IRequest<Unit>
    {
        public string Descricao { get; set; }      
        public string TipoProduto { get; set; }
        public decimal Preco { get; set; }
    }
}