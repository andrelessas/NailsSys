using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.RemoverItem
{
    public class RemoverItemCommandHandler : IRequestHandler<RemoverItemCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public RemoverItemCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(RemoverItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWorks.ItemAgendamento.ObterPorIDAsync(request.Id);

            if(item == null)
                throw new ExcecoesPersonalizadas("Item não encontrado.");

            _unitOfWorks.ItemAgendamento.ExcluirAsync(item);
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}