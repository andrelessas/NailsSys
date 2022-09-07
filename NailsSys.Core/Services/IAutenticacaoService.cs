using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Enums;

namespace NailsSys.Core.Services
{
    public interface IAutenticacaoService
    {
        string GerarToken(int codUsuario, string cargos);
        string ConverteSha256Hash(string senha);
    }
}