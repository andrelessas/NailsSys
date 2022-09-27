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
        private readonly IUnitOfWorks _unitOfWorks;

        public BloquearClienteCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(BloquearClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _unitOfWorks.Cliente.ObterPorIDAsync(request.Id);
            cliente.BloquearCliente();
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}