using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.ProdutoCommands.AlterarProduto
{
    public class AlterarProdutoCommandHandler : IRequestHandler<AlterarProdutoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public AlterarProdutoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(AlterarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _unitOfWorks.Produto.ObterPorIDAsync(request.Id);

            if(produto == null)
                throw new ExcecoesPersonalizadas("Produto não encontrado.");

            produto.AlterarProduto(request.Descricao,request.TipoProduto,request.Preco);
            
            await _unitOfWorks.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}