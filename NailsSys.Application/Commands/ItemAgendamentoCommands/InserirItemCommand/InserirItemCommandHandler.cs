using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.InserirItemCommand
{
    public class InserirItemCommandHandler : IRequestHandler<InserirItemCommand, Unit>
    {
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public InserirItemCommandHandler(IItemAgendamentoRepository itemAgendamentoRepository,IProdutoRepository produtoRepository)
        {
            _itemAgendamentoRepository = itemAgendamentoRepository;
            _produtoRepository = produtoRepository;
        }
        public async Task<Unit> Handle(InserirItemCommand request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorIDAsync(request.IdProduto);
            var maxItem = await _itemAgendamentoRepository.ObterMaxItem(request.IdAgendamento);
            await _itemAgendamentoRepository.InserirItemAsync(new ItemAgendamento(request.IdAgendamento,request.IdProduto,request.Quantidade,maxItem + 1));
            await _itemAgendamentoRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}