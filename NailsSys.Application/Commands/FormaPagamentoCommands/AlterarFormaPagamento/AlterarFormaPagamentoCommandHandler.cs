using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.FormaPagamentoCommands.AlterarFormaPagamento
{
    public class AlterarFormaPagamentoCommandHandler : IRequestHandler<AlterarFormaPagamentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;
        public AlterarFormaPagamentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(AlterarFormaPagamentoCommand request, CancellationToken cancellationToken)
        {
            var formaPagamento = await _unitOfWorks.FormaPagamento.ObterPorIDAsync(request.Id);
            
            if(formaPagamento == null)
                throw new ExcecoesPersonalizadas("Nenhuma forma de pagamento encontrada.");
            
            formaPagamento.AlterarFormaPagamento(request.Descricao,request.AVistaAPrazo);

            await _unitOfWorks.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}