using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Enums;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Core.Entities
{
    public class Atendimento:BaseEntity
    {
        public Atendimento(int idCliente, DateTime dataAtendimento, int idFormaPagamento, DateTime inicioAtendimento, DateTime terminoAtendimento)
        {
            IdCliente = idCliente;
            DataAtendimento = dataAtendimento;
            IdFormaPagamento = idFormaPagamento;
            InicioAtendimento = inicioAtendimento;
            TerminoAtendimento = terminoAtendimento;

            Status = Status.Aberto;
            ItensAtendimento = new List<ItemAtendimento>();
        }

        public Atendimento(int idCliente, DateTime dataAtendimento, int idFormaPagamento, bool atendimentoRealizado, DateTime inicioAtendimento, DateTime terminoAtendimento)
        {
            IdCliente = idCliente;
            DataAtendimento = dataAtendimento;
            IdFormaPagamento = idFormaPagamento;
            AtendimentoRealizado = atendimentoRealizado;
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
        public void CancelarAtendimento()
        {
            if(Status == Status.Cancelado || !AtendimentoRealizado)
                throw new ExcecoesPersonalizadas("Para cancelar o Atendimento, é necessário que o atendimento esteja realizado.");

            switch (Status)
            {
                case Status.Realizado: 
                    if(AtendimentoRealizado) 
                        Status = Status.Cancelado;
                    break;
            }
        }

        public void RealizarAtendimento()
        {
            if(Status == Status.Cancelado || Status == Status.Realizado || AtendimentoRealizado)
                throw new ExcecoesPersonalizadas("Para finalizar o atendimento, é necessário que o mesmo esteja em aberto.");
                
            switch (Status)
            {
                case Status.Realizado: 
                    if(!AtendimentoRealizado) 
                    {
                        AtendimentoRealizado = true;
                        Status = Status.Cancelado;
                    }
                    break;
            }            
        }

        public void AtualizarValores(decimal valorBruto, decimal desconto, decimal valorLiquido)
        {            
            ValorBruto = ItensAtendimento.Sum(x=> x.ValorBruto);
            Desconto = ItensAtendimento.Sum(x=> x.Desconto);
            ValorLiquido = ItensAtendimento.Sum(x=> x.ValorLiquido);
        }

        public void AlterarAtendimento(int idFormaPagamento, int idCliente, DateTime dataAtendimento, DateTime inicioAtendimento, DateTime terminoAtendimento)
        {
            this.IdFormaPagamento = idFormaPagamento;
            this.IdCliente = idCliente;
            this.DataAtendimento = dataAtendimento;
            this.InicioAtendimento = inicioAtendimento;
            this.TerminoAtendimento = terminoAtendimento;
        }
    }
}