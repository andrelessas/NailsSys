using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorData
{
    public class ObterAgendamentosPorDataQueries:IRequest<IEnumerable<AgendamentoViewModel>>
    {
        public ObterAgendamentosPorDataQueries(DateTime data)
        {
            Data = data;
        }

        public DateTime Data { get; private set; }        
    }
}