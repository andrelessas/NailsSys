using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.ItemAtendimentoCommands.RemoverItem
{
    public class RemoverItemAtendimentoCommandHandler : IRequestHandler<RemoverItemAtendimentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public RemoverItemAtendimentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(RemoverItemAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var item = await _unitOfWorks.ItemAtendimento.ObterItemAtendimento(request.Item,request.IdAtendimento);
            if(item == null)
                throw new ExcecoesPersonalizadas("Nenhum item encontrado.");

            var atendimento = await _unitOfWorks.Atendimento.ObterPorIDAsync(request.IdAtendimento);
            
            await _unitOfWorks.BeginTransactionAsync();
            
            _unitOfWorks.ItemAtendimento.ExcluirAsync(item);
            
            atendimento.AtualizarValores(
                // await _unitOfWorks.ItemAtendimento.SumAsync(x=>x.IdAtendimento == atendimento.Id,x=>x.ValorBruto),
                // await _unitOfWorks.ItemAtendimento.SumAsync(x=>x.IdAtendimento == atendimento.Id,x=>x.Desconto),
                // await _unitOfWorks.ItemAtendimento.SumAsync(x=>x.IdAtendimento == atendimento.Id,x=>x.ValorLiquido)
                );
            
            await _unitOfWorks.SaveChangesAsync();
            await _unitOfWorks.CommitAsync();
            return Unit.Value;
        }
    }
}