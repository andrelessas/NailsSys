using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Models;

namespace NailsSys.Application.Queries.ClienteQueries.ObterClientes
{
    public class ObterClientesQueries:IRequest<PaginationResult<ClienteViewModel>>
    {
        public ObterClientesQueries(int page)
        {
            Page = page;
        }

        public int Page { get; set; }    
    }
}