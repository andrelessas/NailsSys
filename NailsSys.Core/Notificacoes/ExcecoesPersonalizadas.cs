using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.Notificacoes
{
    public class ExcecoesPersonalizadas:Exception
    {
        public int StatusCode { get; set; }

        public ExcecoesPersonalizadas(string mensagem)
            :base(mensagem)
        {}

        public ExcecoesPersonalizadas(string mensagem, int statusCode)
            :base(mensagem)
        {
            StatusCode = statusCode;
        }
    }
}