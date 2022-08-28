using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Enums;

namespace NailsSys.Core.Entities
{
    public class Atendimento:BaseEntity
    {
        public Atendimento(int idCliente, DateTime dataAtendimento, bool atendimentoRealizado, int idFormaPagamento, DateTime inicioAtendimento, DateTime terminoAtendimento)
        {
            IdCliente = idCliente;
            DataAtendimento = dataAtendimento;
            AtendimentoRealizado = atendimentoRealizado;
            IdFormaPagamento = idFormaPagamento;
            InicioAtendimento = inicioAtendimento;
            TerminoAtendimento = terminoAtendimento;

            Status = Status.Aberto;
            ItensAtendimento = new List<ItemAtendimento>();
        }

        public int IdCliente { get; private set; }     
        public Cliente Cliente { get; private set; }   
        public DateTime DataAtendimento { get; private set; }
        public bool AtendimentoRealizado { get; private set; }
        public int IdFormaPagamento { get; set; }
        public FormaPagamento FormaPagamento { get; private set; }
        public Status Status { get; private set; }
        public DateTime InicioAtendimento { get; private set; }
        public DateTime TerminoAtendimento { get; private set; }
        public decimal ValorBruto { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal ValorLiquido { get; private set; }
        public List<ItemAtendimento> ItensAtendimento { get; private set; }
    }
}