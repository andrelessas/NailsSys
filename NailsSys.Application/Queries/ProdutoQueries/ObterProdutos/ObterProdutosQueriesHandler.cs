using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Infrastructure.Context;

namespace NailsSys.Application.Queries.ProdutoQueries.ObterProdutos
{
    public class ObterProdutosQueriesHandler : IRequestHandler<ObterProdutosQueries, IEnumerable<ProdutoViewModel>>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ObterProdutosQueriesHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<IEnumerable<ProdutoViewModel>> Handle(ObterProdutosQueries request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterTodosAsync();
            return produto.Select(p => new ProdutoViewModel(p.Id,
                                                            p.Descricao,
                                                            p.TipoProduto,
                                                            p.Preco,
                                                            p.Descontinuado))
                          .ToList();
        }
    }
}