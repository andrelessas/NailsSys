using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItens
{
    public class ObterItensQueries:IRequest<IEnumerable<ItemAgendamentoViewModel>>
    {
        public ObterItensQueries(int idAgendamento)
        {
            IdAgendamento = idAgendamento;
        }

        public int IdAgendamento { get; private set; }   
    }
}