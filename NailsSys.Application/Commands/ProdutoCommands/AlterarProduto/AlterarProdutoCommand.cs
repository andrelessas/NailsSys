using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.ProdutoCommands.AlterarProduto
{
    public class AlterarProdutoCommand:IRequest<Unit>
    {
        public int Id { get; set; }
        public string Descricao{ get; set; }   
        public string TipoProduto { get; set; }
        public decimal Preco { get; set; }
    }
}