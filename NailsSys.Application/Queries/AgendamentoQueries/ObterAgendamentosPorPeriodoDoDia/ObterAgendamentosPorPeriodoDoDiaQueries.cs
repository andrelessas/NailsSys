using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorPeriodoDoDia
{
    public class ObterAgendamentosPorPeriodoDoDiaQueries:IRequest<IEnumerable<AgendamentoViewModel>>
    {
        public ObterAgendamentosPorPeriodoDoDiaQueries(DateTime periodoInicial, DateTime periodoFinal)
        {
            PeriodoInicial = periodoInicial;
            PeriodoFinal = periodoFinal;
        }

        public DateTime PeriodoInicial { get; set; }
        public DateTime PeriodoFinal { get; set; }
    }
}