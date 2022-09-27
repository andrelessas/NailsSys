using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.AlterarItem
{
    public class AlterarItemCommandHandler : IRequestHandler<AlterarItemCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public AlterarItemCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(AlterarItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWorks.ItemAgendamento.ObterPorIDAsync(request.Id);

            if(item == null)
                throw new ExcecoesPersonalizadas("Item não encontrado.");

            item.AlterarQuantidade(request.Quantidade);
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}