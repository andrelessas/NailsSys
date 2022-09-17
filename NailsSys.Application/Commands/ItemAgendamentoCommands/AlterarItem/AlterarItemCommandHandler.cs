using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.AlterarItem
{
    public class AlterarItemCommandHandler : IRequestHandler<AlterarItemCommand, Unit>
    {
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;

        public AlterarItemCommandHandler(IItemAgendamentoRepository itemAgendamentoRepository)
        {
            _itemAgendamentoRepository = itemAgendamentoRepository;
        }
        public async Task<Unit> Handle(AlterarItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemAgendamentoRepository.ObterPorIDAsync(request.Id);

            if(item == null)
                throw new ExcecoesPersonalizadas("Item não encontrado.");

            item.AlterarQuantidade(request.Quantidade);
            await _itemAgendamentoRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}