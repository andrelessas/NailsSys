using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.Entities
{
    public class Cliente:BaseEntity
    {
        public Cliente(string nomeCliente, string telefone)
        {
            NomeCliente = nomeCliente;
            Telefone = telefone;
            DataCadastro = DateTime.Now;
        }

        public string NomeCliente { get; private set; }
        public string Telefone { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Bloqueado { get; private set; }
        public List<Agendamento> Agendamentos { get; private set; }

        public void BloquearCliente()
        {
            if(!Bloqueado && DataCadastro != null)
                Bloqueado = true;
        }

        public void AlterarCliente(string nomeCliente, string telefone)
        {
            NomeCliente = nomeCliente;
            Telefone = telefone;
        }
    }
}