using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.InputModels
{
    public class AlterarProdutoInputModel
    {
        public int Id { get; set; }
        public string Descricao{ get; set; }   
        public string TipoProduto { get; set; }
        public decimal Preco { get; set; }
    }
}