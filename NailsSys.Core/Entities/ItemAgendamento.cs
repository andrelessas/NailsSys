using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.Entities
{
    public class ItemAgendamento:BaseEntity
    {
        public ItemAgendamento(int idProduto, int idAgendamento, int quantidade, decimal precoInicial)
        {
            IdProduto = idProduto;
            IdAgendamento = idAgendamento;
            Quantidade = quantidade;
            PrecoInicial = precoInicial;
            AtualizarPreco();
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
                PrecoInicial = PrecoInicial * Quantidade;
            }
        }
        public void AtualizarPreco()
        {
            if(Quantidade > 0)
                PrecoInicial = PrecoInicial * Quantidade;
        }
    }
}