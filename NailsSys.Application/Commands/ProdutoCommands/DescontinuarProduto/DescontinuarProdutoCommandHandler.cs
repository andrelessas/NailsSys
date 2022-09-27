using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.ProdutoCommands.DescontinuarProduto
{    
    public class DescontinuarProdutoCommandHandler : IRequestHandler<DescontinuarProdutoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public DescontinuarProdutoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit>Handle(DescontinuarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _unitOfWorks.Produto.ObterPorIDAsync(request.Id);
            
            if(produto == null)
                throw new ExcecoesPersonalizadas("Produto não encontrado.");
            
            produto.DescontinuarProduto();
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}