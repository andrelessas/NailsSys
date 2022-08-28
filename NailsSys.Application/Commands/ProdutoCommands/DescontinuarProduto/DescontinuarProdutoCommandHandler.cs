using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Infrastructure.Context;

namespace NailsSys.Application.Commands.ProdutoCommands.DescontinuarProduto
{    
    public class DescontinuarProdutoCommandHandler : IRequestHandler<DescontinuarProdutoCommand, Unit>
    {
        private readonly IProdutoRepository _produtoRepository;

        public DescontinuarProdutoCommandHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<Unit> Handle(DescontinuarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorIDAsync(request.Id);
            produto.DescontinuarProduto();
            await _produtoRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}