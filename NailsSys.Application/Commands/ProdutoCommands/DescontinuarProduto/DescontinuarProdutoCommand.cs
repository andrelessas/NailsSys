using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.ProdutoCommands.DescontinuarProduto
{
    public class DescontinuarProdutoCommand:IRequest<Unit>
    {
        public DescontinuarProdutoCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }    
    }
}