using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Models;

namespace NailsSys.Application.Queries.AtendimentoQueries.ObterAtendimentosPorPeriodo
{
    public class ObterAtendimentosPorPeriodoQueries:IRequest<IEnumerable<AtendimentoViewModel>>
    {
        public ObterAtendimentosPorPeriodoQueries(DateTime dataInicial, DateTime dataFinal)
        {
            DataInicial = dataInicial;
            DataFinal = dataFinal;
        }

        public DateTime DataInicial { get; set; }        
        public DateTime DataFinal { get; set; }
    }
}