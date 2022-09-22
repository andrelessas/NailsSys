using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Core.Notificacoes;

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

            if(produto == null || produto.Data.Count() == 0)
                throw new ExcecoesPersonalizadas("Nenhum produto encontrado.");
                
            return _mapper.Map<PaginationResult<ProdutoViewModel>>(produto);
        }
    }
}