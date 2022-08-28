using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.ClienteQueries.ObterClientePorId
{
    public class ObterClientePorIdQueriesHandler : IRequestHandler<ObterClientePorIdQueries, ClienteViewModel>
    {
        private readonly IClienteRepository _clienteRepository;

        public ObterClientePorIdQueriesHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<ClienteViewModel> Handle(ObterClientePorIdQueries request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.ObterPorIDAsync(request.Id);
            return new ClienteViewModel(cliente.Id,cliente.NomeCliente,cliente.Telefone,cliente.Bloqueado);
        }
    }
}