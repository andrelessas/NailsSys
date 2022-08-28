using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.ProdutoQueries.ObterProdutos
{
    public class ObterProdutosQueries:IRequest<IEnumerable<ProdutoViewModel>>
    {
        
    }
}