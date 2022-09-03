using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.ClienteQueries.ObterClientePorId
{
    public class ObterClientePorIdQueriesHandler : IRequestHandler<ObterClientePorIdQueries, ClienteViewModel>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ObterClientePorIdQueriesHandler(IClienteRepository clienteRepository,
                                                IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
        public async Task<ClienteViewModel> Handle(ObterClientePorIdQueries request, CancellationToken cancellationToken)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterPorIDAsync(request.Id));
        }
    }
}