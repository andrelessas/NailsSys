using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Enums;

namespace NailsSys.Core.Entities
{
    public class Usuario:BaseEntity
    {
        public Usuario(string nomeCompleto, string login, string senha, string cargo)
        {
            NomeCompleto = nomeCompleto;
            Login = login;
            Senha = senha;
            Cargo = cargo;
        }

        public string NomeCompleto { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Cargo { get; private set; }
    }
}