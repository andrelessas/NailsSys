using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;

namespace NailsSys.Application.Queries.ClienteQueries.ObterClientes
{
    public class ObterClientesQueriesHandler : IRequestHandler<ObterClientesQueries,PaginationResult<ClienteViewModel>>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ObterClientesQueriesHandler(IClienteRepository clienteRepository,
                                            IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
        public async Task<PaginationResult<ClienteViewModel>> Handle(ObterClientesQueries request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.ObterTodosAsync(request.Page);
            return _mapper.Map<PaginationResult<ClienteViewModel>>(cliente);
        }
    }
}