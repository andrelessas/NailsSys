using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class ItemAtendimentoViewModel
    {
        public int Item { get; private set; }
        public string NomeProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorBruto { get; private set; }      
        public decimal Desconto { get; private set; }
        public decimal ValorLiquido { get; private set; }
    }
}