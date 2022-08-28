using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.Entities
{
    public class ItemAtendimento:BaseEntity
    {
        public ItemAtendimento(int idAtendimento, int idProduto, decimal quantidade)
        {
            IdAtendimento = idAtendimento;
            IdProduto = idProduto;
            Quantidade = quantidade;            
            Item = Id;
        }

        public int Item { get; private set; }
        public int IdAtendimento { get; private set; }
        public int IdProduto { get; set; }
        public decimal Quantidade { get; private set; }
        public decimal ValorBruto { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorLiquido { get; private set; }
    }
}