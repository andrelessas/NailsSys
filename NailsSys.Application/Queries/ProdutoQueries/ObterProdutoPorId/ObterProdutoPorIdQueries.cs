using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.ProdutoQueries.ObterProdutoPorId
{
    public class ObterProdutoPorIdQueries:IRequest<ProdutoViewModel>
    {
        public ObterProdutoPorIdQueries(int id)
        {
            Id = id;
        }

        public int Id { get; set; }   
    }
}