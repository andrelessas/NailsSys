using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class ClienteViewModel
    {
        public ClienteViewModel(int id, string nomeCliente, string telefone, bool bloqueado)
        {
            Id = id;
            NomeCliente = nomeCliente;
            Telefone = telefone;
            Bloqueado = bloqueado;
        }

        public int Id { get; private set; }
        public string NomeCliente { get; private set; }
        public string Telefone { get; private set; }
        public bool Bloqueado { get; private set; }
    }
}