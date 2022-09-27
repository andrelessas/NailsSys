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
        private readonly IUnitOfWorks _unitOfWorks;

        public InserirClienteCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(InserirClienteCommand request, CancellationToken cancellationToken)
        {
            _unitOfWorks.Cliente.InserirAsync(new Cliente(request.NomeCliente,request.Telefone));
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}