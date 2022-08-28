using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.ClienteCommands.AlterarCliente
{
    public class AlterarClienteCommandHandler : IRequestHandler<AlterarClienteCommand, Unit>
    {
        private readonly IClienteRepository _clienteRepository;

        public AlterarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<Unit> Handle(AlterarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.ObterPorIDAsync(request.Id);
            cliente.AlterarCliente(request.NomeCliente,request.Telefone);
            await _clienteRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}