using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class ProdutoItemAgendViewModel
    {
        public ProdutoItemAgendViewModel(string descricao, decimal preco, int idProduto)
        {
            Descricao = descricao;
            Preco = preco;
            IdProduto = idProduto;
        }
        public int IdProduto { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
    }
}