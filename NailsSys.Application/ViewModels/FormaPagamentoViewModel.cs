using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class FormaPagamentoViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string AVistaAPrazo { get; set; }
        public bool Descontinuado { get; set; }
    }
}