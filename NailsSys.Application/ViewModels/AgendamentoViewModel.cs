using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class AgendamentoViewModel
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataAtendimento { get; set; }
        public DateTime InicioPrevisto { get; set; }
        public DateTime TerminoPrevisto { get; set; }
        public List<ItemAgendamentoViewModel> Itens { get; set; }
    }
}