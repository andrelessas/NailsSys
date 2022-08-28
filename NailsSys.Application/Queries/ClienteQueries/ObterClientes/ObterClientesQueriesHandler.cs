using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.ClienteQueries.ObterClientes
{
    public class ObterClientesQueriesHandler : IRequestHandler<ObterClientesQueries,IEnumerable<ClienteViewModel>>
    {
        private readonly IClienteRepository _clienteRepository;

        public ObterClientesQueriesHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<IEnumerable<ClienteViewModel>> Handle(ObterClientesQueries request, CancellationToken cancellationToken)
        {
            var clientes = await _clienteRepository.ObterTodosAsync();
            return clientes.Select(c=> new ClienteViewModel(c.Id,
                                                            c.NomeCliente,
                                                            c.Telefone,
                                                            c.Bloqueado))
                           .ToList();
        }
    }
}