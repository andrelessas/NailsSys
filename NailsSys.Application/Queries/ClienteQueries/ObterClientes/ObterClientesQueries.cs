using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.ClienteQueries.ObterClientes
{
    public class ObterClientesQueries:IRequest<IEnumerable<ClienteViewModel>>
    {
        
    }
}