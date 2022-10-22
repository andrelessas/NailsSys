using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Models;

namespace NailsSys.Application.Queries.ItemAtendimentoQueries.ObterItens
{
    public class ObterItensAtendimentoQueries:IRequest<PaginationResult<ItemAtendimentoViewModel>>
    {
        public ObterItensAtendimentoQueries(int idAtendimento, int page)
        {
            IdAtendimento = idAtendimento;
            Page = page;
        }

        public int IdAtendimento { get; set; }
        public int Page { get; set; }
    }
}