using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItemPorId
{
    public class ObterItemPorIdQueries:IRequest<ItemAgendamentoViewModel>
    {
        public ObterItemPorIdQueries(int idAgendamento)
        {
            IdAgendamento = idAgendamento;
        }

        public int IdAgendamento { get; private set; }
    }
}