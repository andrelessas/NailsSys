using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.RemoverItem
{
    public class RemoverItemCommandHandler : IRequestHandler<RemoverItemCommand, Unit>
    {
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;

        public RemoverItemCommandHandler(IItemAgendamentoRepository itemAgendamentoRepository)
        {
            _itemAgendamentoRepository = itemAgendamentoRepository;
        }
        public async Task<Unit> Handle(RemoverItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _itemAgendamentoRepository.ObterPorIDAsync(request.Id);
            _itemAgendamentoRepository.ExcluirAsync(item);
            await _itemAgendamentoRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}