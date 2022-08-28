using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.InputModels
{
    public class AlterarAgendamentoInputModel
    {
        public int Id { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public DateTime DataAtendimento { get; set; }
        public DateTime InicioPrevisto { get; set; }
        public DateTime TerminoPrevisto { get; set; }
        public List<ProdutoViewModel> Produtos { get; set; }
    }
}