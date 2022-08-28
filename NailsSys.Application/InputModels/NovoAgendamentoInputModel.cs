using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;

namespace NailsSys.Application.InputModels
{
    public class NovoAgendamentoInputModel
    {
        public int idCliente { get; set; }
        public DateTime DataAtendimento { get; set; }
        public DateTime InicioPrevisto { get; set; }
        public DateTime TerminoPrevisto { get; set; }
    }
}