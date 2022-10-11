using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.AlterarItem
{
    public class AlterarItemAgendamentoCommandHandler : IRequestHandler<AlterarItemAgendamentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public AlterarItemAgendamentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(AlterarItemAgendamentoCommand request, CancellationToken cancellationToken)
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