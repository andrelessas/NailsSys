using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            _itemAgendamentoRepository.InserirAsync(new ItemAgendamento(request.IdAgendamento,request.IdProduto,request.Quantidade,produto.Preco));
            await _itemAgendamentoRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}