using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.FormaPagamentoCommands.DescontinuarFormaPagamento
{
    public class DescontinuarFormaPagamentoCommandHandler : IRequestHandler<DescontinuarFormaPagamentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public DescontinuarFormaPagamentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(DescontinuarFormaPagamentoCommand request, CancellationToken cancellationToken)
        {
            var formaPagamento = await _unitOfWorks.FormaPagamento.ObterPorIDAsync(request.Id);

            if(formaPagamento == null)
                throw new ExcecoesPersonalizadas("Forma de pagamento não encontrada.");

            formaPagamento.DescontinuarFormaPagamento();
            await _unitOfWorks.SaveChangesAsync();

            return Unit.Value;
        }
    }
}