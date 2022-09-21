using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Models;

namespace NailsSys.Application.Queries.ProdutoQueries.ObterProdutos
{
    public class ObterProdutosQueries:IRequest<PaginationResult<ProdutoViewModel>>
    {
        public ObterProdutosQueries(int page)
        {
            Page = page;
        }

        public int Page { get; set; }   
    }
}