using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.Services
{
    public interface IAutenticacaoService
    {
        string GerarToken(int codUsuario, string role);
        string ConverteSha256Hash(string senha);
    }
}