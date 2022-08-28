using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.ClienteCommands.InserirCliente
{
    public class InserirClienteCommandHandler : IRequestHandler<InserirClienteCommand, Unit>
    {
        private readonly IClienteRepository _clienteRepository;

        public InserirClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<Unit> Handle(InserirClienteCommand request, CancellationToken cancellationToken)
        {
            _clienteRepository.InserirAsync(new Cliente(request.NomeCliente,request.Telefone));
            await _clienteRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}