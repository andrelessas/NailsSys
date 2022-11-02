using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.AtendimentoCommands.NovoAtendimento
{
    public class NovoAtendimentoCommandHandler : IRequestHandler<NovoAtendimentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public NovoAtendimentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(NovoAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var formaPagamento = await _unitOfWorks.FormaPagamento.ObterPorIDAsync(request.IdFormaPagamento);

            if (formaPagamento == null)
                throw new ExcecoesPersonalizadas("A forma de pagamento informada não existe.");

            var cliente = await _unitOfWorks.Cliente.ObterPorIDAsync(request.IdCliente);

            if (cliente == null)
                throw new ExcecoesPersonalizadas("O cliente informado não existe.");

            var idAtendimento = await _unitOfWorks.Atendimento.MaxAsync(x => x.Id) + 1;

            await _unitOfWorks.BeginTransactionAsync();
            var atendimento = new Atendimento(
                request.IdCliente,
                request.DataAtendimento,
                request.IdFormaPagamento,
                request.AtendimentoRealizado,
                request.InicioAtendimento,
                request.TerminoAtendimento);


            _unitOfWorks.Atendimento.InserirAsync(atendimento);
            await _unitOfWorks.SaveChangesAsync();

            if (request.Itens == null || !request.Itens.Any())
                throw new ExcecoesPersonalizadas("Nenhum produto informado para lançar o atendimento.");
            
            await InserirItens(request, idAtendimento,atendimento);
            await _unitOfWorks.SaveChangesAsync();
            await _unitOfWorks.CommitAsync();

            return Unit.Value;
        }

        private async Task InserirItens(NovoAtendimentoCommand request, int idAtendimento, Atendimento atendimento)
        {
            foreach (var item in request.Itens)
            {
                var maxItem = await _unitOfWorks.ItemAtendimento.MaxAsync(x => x.IdAtendimento == idAtendimento, i => i.Id) + 1;

                var produto = await _unitOfWorks.Produto.ObterPorIDAsync(item.IdProduto);

                if (produto == null)
                    throw new ExcecoesPersonalizadas("Produto informado não existe.");

                var itemAtendimento = new ItemAtendimento(idAtendimento, item.IdProduto, item.Quantidade, maxItem);
                await _unitOfWorks.ItemAtendimento.InserirItemAtendimento(itemAtendimento);
                await _unitOfWorks.SaveChangesAsync();
                atendimento.AtualizarValores();
            }
        }
    }
}