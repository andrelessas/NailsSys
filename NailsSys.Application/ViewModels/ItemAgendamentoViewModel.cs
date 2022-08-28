using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class ItemAgendamentoViewModel
    {
        public ItemAgendamentoViewModel(ProdutoItemAgendViewModel produto, int quantidade, decimal precoInicial, int item)
        {
            Produto = produto;
            Quantidade = quantidade;
            PrecoInicial = precoInicial;
            Item = item;
        }
        public int Item { get; private set; }
        public ProdutoItemAgendViewModel Produto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoInicial { get; private set; }  
    }
}