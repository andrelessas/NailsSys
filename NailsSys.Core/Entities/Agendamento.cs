using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Core.Entities
{
    public class Agendamento:BaseEntity
    {
        public Agendamento(int idCliente, DateTime dataAtendimento, DateTime inicioPrevisto, DateTime terminoPrevisto)
        {
            IdCliente = idCliente;
            DataAtendimento = dataAtendimento;
            InicioPrevisto = inicioPrevisto;
            TerminoPrevisto = terminoPrevisto;

            ItemAgendamentos = new List<ItemAgendamento>();
        }

        public int IdCliente { get; private set; }  
        public Cliente Cliente { get; private set; }        
        public DateTime DataAtendimento { get; private set; }
        public DateTime InicioPrevisto { get; private set; }
        public DateTime TerminoPrevisto { get; private set; }
        public DateTime AtendimentoFinalizado { get; private set; }
        public bool Cancelado { get; private set; }
        public bool AtendimentoRealizado { get; private set; }
        public List<ItemAgendamento> ItemAgendamentos { get; private set; }

        public void CancelarAgendamento()
        {
            if(!Cancelado && !AtendimentoRealizado)
                Cancelado = true;
            else 
                throw new ExcecoesPersonalizadas("para cancelar o agendamento, é necessário que o mesmo esteja em aberto.");            
        }
        public void AlterarAgendamento(int idCliente, DateTime dataAtendimento, DateTime inicioPrevisto, DateTime terminoPrevisto)
        {
            IdCliente = idCliente;
            DataAtendimento = dataAtendimento;
            InicioPrevisto = inicioPrevisto;
            TerminoPrevisto = terminoPrevisto;    
        }

        public void RealizarAtendimento()
        {
            if(!AtendimentoRealizado)
            AtendimentoRealizado = true;
        }
    }
}