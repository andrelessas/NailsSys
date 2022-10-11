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
            
            if(formaPagamento == null)
                throw new ExcecoesPersonalizadas("A forma de pagamento informada não existe.");

            await _unitOfWorks.BeginTransactionAsync();
            _unitOfWorks.Atendimento.InserirAsync(new Atendimento(
                request.IdCliente,
                request.DataAtendimento,
                request.IdFormaPagamento,
                request.AtendimentoRealizado,
                request.InicioAtendimento,
                request.TerminoAtendimento));

            // await _unitOfWorks.SaveChangesAsync();            

            if(request.Itens == null || !request.Itens.Any())
                throw new ExcecoesPersonalizadas("Nenhum produto informado para lançar o atendimento.");

            var idAtendimento = await _unitOfWorks.Atendimento.MaxAsync(x => x.Id);
            foreach (var item in request.Itens)
            {
                var maxItem = await _unitOfWorks.ItemAtendimento.MaxAsync(x => x.IdAtendimento == idAtendimento, i => i.Id);
                _unitOfWorks.ItemAtendimento.InserirAsync(new ItemAtendimento(idAtendimento+1, item.IdProduto, item.Quantidade, maxItem + 1));
            }
            
            await _unitOfWorks.SaveChangesAsync();            
            await _unitOfWorks.CommitAsync();

            return Unit.Value;
        }
    }
}