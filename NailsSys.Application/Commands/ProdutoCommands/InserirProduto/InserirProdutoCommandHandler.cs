using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.ProdutoCommands.InserirProduto
{
    public class InserirProdutoCommandHandler : IRequestHandler<InserirProdutoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public InserirProdutoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(InserirProdutoCommand request, CancellationToken cancellationToken)
        {
            _unitOfWorks.Produto.InserirAsync(new Produto(request.Descricao, request.TipoProduto, request.Preco));
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}