using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.DTOs
{
    public class ItemAgendamentoDTO
    {
        public ItemAgendamentoDTO(int id, int idAgendamento, int idProduto, string descricaoProduto, int quantidade, decimal precoInicial)
        {
            Id = id;
            IdAgendamento = idAgendamento;
            IdProduto = idProduto;
            DescricaoProduto = descricaoProduto;
            Quantidade = quantidade;
            PrecoInicial = precoInicial;
        }

        public int Id { get; set; }
        public int IdAgendamento { get; set; }
        public int IdProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoInicial { get; set; }   
    }
}