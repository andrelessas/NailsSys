using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.FormaPagamentoCommands.InserirFormaPagamento
{
    public class InserirFormaPagamentoCommandHandler : IRequestHandler<InserirFormaPagamentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public InserirFormaPagamentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(InserirFormaPagamentoCommand request, CancellationToken cancellationToken)
        {
            _unitOfWorks.FormaPagamento.InserirAsync(new FormaPagamento(request.Descricao,request.AVistaAPrazo));
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}