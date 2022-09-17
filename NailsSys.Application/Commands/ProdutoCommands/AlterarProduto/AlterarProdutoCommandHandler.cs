using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.Infrastructure.Context;

namespace NailsSys.Application.Commands.ProdutoCommands.AlterarProduto
{
    public class AlterarProdutoCommandHandler : IRequestHandler<AlterarProdutoCommand, Unit>
    {
        private readonly IProdutoRepository _produtoRepository;

        public AlterarProdutoCommandHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<Unit> Handle(AlterarProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorIDAsync(request.Id);

            if(produto == null)
                throw new ExcecoesPersonalizadas("Produto não encontrado.");

            produto.AlterarProduto(request.Descricao,request.TipoProduto,request.Preco);
            
            await _produtoRepository.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}