using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.InputModels
{
    public class AlterarClienteInputModel
    {
        public int Id { get; set; }
        public string NomeCliente { get; set; }
        public string Telefone { get; set; }
    }
}