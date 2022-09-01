using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.DTOs
{
    public class ItemAgendamentoDTO
    {
        public ItemAgendamentoDTO(int id, int idAgendamento, int idProduto, string descricaoProduto, int quantidade, decimal precoInicial, int item)
        {
            Id = id;
            IdAgendamento = idAgendamento;
            IdProduto = idProduto;
            DescricaoProduto = descricaoProduto;
            Quantidade = quantidade;
            PrecoInicial = precoInicial;
            Item = item;
        }

        public int Id { get; private set; }
        public int IdAgendamento { get; private set; }
        public int IdProduto { get; private set; }
        public int Item { get; private set; }
        public string DescricaoProduto { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoInicial { get; private set; }   
    }
}