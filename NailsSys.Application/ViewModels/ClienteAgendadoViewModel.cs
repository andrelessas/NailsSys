using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class ClienteAgendadoViewModel
    {
        public ClienteAgendadoViewModel(int id, string nomeCliente, string telefone)
        {
            Id = id;
            NomeCliente = nomeCliente;
            Telefone = telefone;
        }

        public int Id { get; private set; }
        public string NomeCliente { get; private set; }
        public string Telefone { get; private set; }
    }
}