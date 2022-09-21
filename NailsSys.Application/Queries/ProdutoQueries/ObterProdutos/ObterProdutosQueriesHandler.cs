using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;

namespace NailsSys.Application.Queries.ProdutoQueries.ObterProdutos
{
    public class ObterProdutosQueriesHandler : IRequestHandler<ObterProdutosQueries, PaginationResult<ProdutoViewModel>>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ObterProdutosQueriesHandler(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }
        public async Task<PaginationResult<ProdutoViewModel>> Handle(ObterProdutosQueries request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterTodosAsync(request.Page);
            return _mapper.Map<PaginationResult<ProdutoViewModel>>(produto);
        }
    }
}