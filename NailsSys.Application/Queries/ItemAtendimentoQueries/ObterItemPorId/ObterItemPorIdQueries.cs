using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.ItemAtendimentoQueries.ObterItemPorId
{
    public class ObterItemPorIdQueries:IRequest<ItemAtendimentoViewModel>
    {

        public ObterItemPorIdQueries(int item, int idAtendimento)
        {
            Item = item;
            IdAtendimento = idAtendimento;
        }
        public int IdAtendimento { get; set; }
        public int Item { get; set; }
        
    }
}