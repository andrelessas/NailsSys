using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.ClienteQueries.ObterClientes
{
    public class ObterClientesQueriesHandler : IRequestHandler<ObterClientesQueries,IEnumerable<ClienteViewModel>>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ObterClientesQueriesHandler(IClienteRepository clienteRepository,
                                            IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ClienteViewModel>> Handle(ObterClientesQueries request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodosAsync());
        }
    }
}