using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.InputModels
{
    public class NovoItemAgendamentoInputModel
    {
        public int IdProduto { get; set; }
        public int IdAgendamento { get; set; }
        public int Quantidade { get; set; }
    }
}