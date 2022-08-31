using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class ItemAgendamentoViewModel
    {
        public ItemAgendamentoViewModel(string nomeProduto, int quantidade, decimal precoInicial, int item)
        {
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            PrecoInicial = precoInicial;
            Item = item;
        }
        public int Item { get; private set; }
        public string NomeProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoInicial { get; private set; }  
    }
}