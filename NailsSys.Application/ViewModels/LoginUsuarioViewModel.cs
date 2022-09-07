using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class LoginUsuarioViewModel
    {
        public LoginUsuarioViewModel(string usuario, string token)
        {
            Usuario = usuario;
            Token = token;
        }

        public string Usuario { get; set; }
        public string Token { get; set; }
    }
}