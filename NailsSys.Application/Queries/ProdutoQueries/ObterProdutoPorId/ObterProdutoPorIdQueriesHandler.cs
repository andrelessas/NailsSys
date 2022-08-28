using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.ProdutoQueries.ObterProdutoPorId
{
    public class ObterProdutoPorIdQueriesHandler : IRequestHandler<ObterProdutoPorIdQueries, ProdutoViewModel>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ObterProdutoPorIdQueriesHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<ProdutoViewModel> Handle(ObterProdutoPorIdQueries request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorIDAsync(request.Id);
            return new ProdutoViewModel(produto.Id,produto.Descricao,produto.TipoProduto,produto.Preco,produto.Descontinuado);
        }
    }
}