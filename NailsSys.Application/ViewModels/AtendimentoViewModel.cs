using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Enums;

namespace NailsSys.Application.ViewModels
{
    public class AtendimentoViewModel
    {
        public int IdAtendimento { get; private set; }
        public string NomeCliente { get; private set; }
        public DateTime DataAtendimento { get; private set; }
        public decimal ValorBruto { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public string DescricaoFormaPagamento { get; private set; }
        public Status StatusAtendimento { get; private set; }
        public List<ItemAtendimentoViewModel> Itens { get; set; }
    }
}