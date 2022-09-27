using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.InserirItemCommand
{
    public class InserirItemCommandHandler : IRequestHandler<InserirItemCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public InserirItemCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(InserirItemCommand request, CancellationToken cancellationToken)
        {
            var produto = await _unitOfWorks.Produto.ObterPorIDAsync(request.IdProduto);
            var maxItem = await _unitOfWorks.ItemAgendamento.ObterMaxItem(request.IdAgendamento);
            await _unitOfWorks.ItemAgendamento.InserirItemAsync(new ItemAgendamento(request.IdAgendamento,request.IdProduto,request.Quantidade,maxItem + 1));
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}