using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.ProdutoCommands.DescontinuarProduto
{
    public class DescontinuarProdutoCommand:IRequest<Unit>
    {
        public int Id { get; set; }    
    }
}