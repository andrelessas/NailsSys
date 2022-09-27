using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Models;

namespace NailsSys.Application.Queries.FormaPagamentoQueries.ObterFormasPagamentos
{
    public class ObterFormasPagamentoQueries:IRequest<PaginationResult<FormaPagamentoViewModel>>
    {
        public ObterFormasPagamentoQueries(int page)
        {
            Page = page;
        }

        public int Page { get; set; }
    }
}