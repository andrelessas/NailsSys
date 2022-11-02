using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.Entities
{
    public class ItemAtendimento:BaseEntity
    {
        public ItemAtendimento(int idAtendimento, int idProduto, int quantidade, int item)
        {
            IdAtendimento = idAtendimento;
            IdProduto = idProduto;
            Quantidade = quantidade;            
            Item = item;
        }

        public int Item { get; private set; }
        public int IdAtendimento { get; private set; }
        public Atendimento Atendimento { get; private set; }
        public int IdProduto { get; set; }
        public Produto Produto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorBruto { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorLiquido { get; private set; }

        public void AtualizarPreco(decimal preco)
        {
            this.ValorBruto = preco * Quantidade;
            this.Desconto = 0;
            this.ValorLiquido = this.ValorBruto;
        }

        public void AlterarQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }        
    }
}