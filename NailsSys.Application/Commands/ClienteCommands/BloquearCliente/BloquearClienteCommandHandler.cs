using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.ClienteCommands.BloquearCliente
{
    public class BloquearClienteCommandHandler : IRequestHandler<BloquearClienteCommand, Unit>
    {
        private readonly IClienteRepository _clienteRepository;

        public BloquearClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<Unit> Handle(BloquearClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.ObterPorIDAsync(request.Id);
            cliente.BloquearCliente();
            await _clienteRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}