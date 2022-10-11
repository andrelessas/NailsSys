using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.ItemAtendimentoCommands.AlterarItem
{
    public class AlterarItemAtendimentoCommandHandler : IRequestHandler<AlterarItemAtendimentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public AlterarItemAtendimentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(AlterarItemAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var atendimento = await _unitOfWorks.Atendimento.ObterPorIDAsync(request.IdAtendimento);
            if (atendimento == null)
                throw new ExcecoesPersonalizadas("Atendimento não encontrado.");

            var item = await _unitOfWorks.ItemAtendimento.ObterItemAtendimento(request.Item, request.IdAtendimento);
            if (item == null)
                throw new ExcecoesPersonalizadas("O atendimento não possui item informado.");

            await _unitOfWorks.BeginTransactionAsync();
            
            item.AlterarQuantidade(request.Quantidade);            

            atendimento.AtualizarValores(
                await _unitOfWorks.ItemAtendimento.SumAsync(x=>x.IdAtendimento == request.IdAtendimento,y => y.ValorBruto),
                await _unitOfWorks.ItemAtendimento.SumAsync(x=>x.IdAtendimento == request.IdAtendimento,y => y.Desconto),
                await _unitOfWorks.ItemAtendimento.SumAsync(x=>x.IdAtendimento == request.IdAtendimento,y => y.ValorLiquido)
            );
            await _unitOfWorks.SaveChangesAsync();
            await _unitOfWorks.CommitAsync();

            return Unit.Value;
        }
    }
}