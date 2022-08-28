using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel(int id, string descricao, string tipoProduto, decimal preco, bool descontinuado)
        {
            Id = id;
            Descricao = descricao;
            TipoProduto = tipoProduto;
            Preco = preco;
            Descontinuado = descontinuado;
        }

        public int Id { get; private set; }   
        public string Descricao { get; private set; }
        public string TipoProduto { get; private set; }
        public decimal Preco { get; private set; }
        public bool Descontinuado { get; private set; }
    }
}