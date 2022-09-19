using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.ProdutoQueries.ObterProdutoPorId
{
    public class ObterProdutoPorIdQueriesHandler : IRequestHandler<ObterProdutoPorIdQueries, ProdutoViewModel>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ObterProdutoPorIdQueriesHandler(IProdutoRepository produtoRepository,IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }
        public async Task<ProdutoViewModel> Handle(ObterProdutoPorIdQueries request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorIDAsync(request.Id);
            return _mapper.Map<ProdutoViewModel>(produto);
            // new ProdutoViewModel(produto.Id,produto.Descricao,produto.TipoProduto,produto.Preco,produto.Descontinuado);
        }
    }
}