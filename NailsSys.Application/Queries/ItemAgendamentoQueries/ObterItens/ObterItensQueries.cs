using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Models;

namespace NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItens
{
    public class ObterItensQueries:IRequest<PaginationResult<ItemAgendamentoViewModel>>
    {
        public ObterItensQueries(int idAgendamento, int page)
        {
            IdAgendamento = idAgendamento;
            Page = page;
        }

        public int IdAgendamento { get; private set; }  
        public int Page { get; private set; }
    }
}