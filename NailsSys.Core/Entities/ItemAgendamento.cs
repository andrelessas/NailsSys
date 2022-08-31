using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.Entities
{
    public class ItemAgendamento:BaseEntity
    {
        public ItemAgendamento(int idAgendamento, int idProduto, int quantidade, decimal precoInicial)
        {
            IdProduto = idProduto;
            IdAgendamento = idAgendamento;
            Quantidade = quantidade;            
            AtualizarPreco(precoInicial);
        }
        public int IdAgendamento { get; private set; }
        public Agendamento Agendamento { get; private set; }
        public int IdProduto { get; private set; }
        public Produto Produto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoInicial { get; private set; }        
        public void AlterarQuantidade(int quantidade)
        {
            if(Quantidade > 0)
            {
                Quantidade = quantidade;
                AtualizarPreco(PrecoInicial);
            }
        }
        public void AtualizarPreco(decimal valor)
        {
            if(Quantidade > 0)
                PrecoInicial = valor * Quantidade;
        }
    }
}