using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Application.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Login { get; set; }
        public string Cargo { get; set; }
    }
}