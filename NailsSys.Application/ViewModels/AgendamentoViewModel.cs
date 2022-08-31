using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class AgendamentoViewModel
    {
        public AgendamentoViewModel(DateTime dataAtendimento, DateTime inicioPrevisto, DateTime terminoPrevisto, string nomeCliente, int id)
        {
            DataAtendimento = dataAtendimento;
            InicioPrevisto = inicioPrevisto;
            TerminoPrevisto = terminoPrevisto;
            NomeCliente = nomeCliente;
            Id = id;
        }

        public int Id { get; private set; }
        public string NomeCliente { get; private set; }
        public DateTime DataAtendimento { get; private set; }
        public DateTime InicioPrevisto { get; private set; }
        public DateTime TerminoPrevisto { get; private set; }
        public List<ItemAgendamentoViewModel> Itens { get; set; }
    }
}