using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.ProdutoCommands.InserirProduto
{
    public class InserirProdutoCommandHandler : IRequestHandler<InserirProdutoCommand, Unit>
    {
        private readonly IProdutoRepository _produtoRepository;

        public InserirProdutoCommandHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<Unit> Handle(InserirProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = new Produto(request.Descricao, request.TipoProduto, request.Preco);
            _produtoRepository.InserirAsync(produto);
            await _produtoRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}