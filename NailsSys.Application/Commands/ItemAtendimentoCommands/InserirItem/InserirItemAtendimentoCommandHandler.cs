using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.ItemAtendimentoCommands.InserirItem
{
    public class InserirItemAtendimentoCommandHandler : IRequestHandler<InserirItemAtendimentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public InserirItemAtendimentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(InserirItemAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var atendimento = await _unitOfWorks.Atendimento.ObterPorIDAsync(request.IdAtendimento);
            if(atendimento == null)
                throw new ExcecoesPersonalizadas("Atendimento não encontrado.");
            
            var item = await _unitOfWorks.ItemAtendimento.MaxAsync(x => x.IdAtendimento == request.IdAtendimento, i => i.Item);

            await _unitOfWorks.BeginTransactionAsync();
            await _unitOfWorks.ItemAtendimento.InserirItemAtendimento(new ItemAtendimento(
                request.IdAtendimento,
                request.IdProduto,
                request.Quantidade,
                item + 1));

            await _unitOfWorks.SaveChangesAsync();

            atendimento.AtualizarValores(
                await _unitOfWorks.ItemAtendimento.SumAsync(x=>x.IdAtendimento == atendimento.Id,x=>x.ValorBruto),
                await _unitOfWorks.ItemAtendimento.SumAsync(x=>x.IdAtendimento == atendimento.Id,x=>x.Desconto),
                await _unitOfWorks.ItemAtendimento.SumAsync(x=>x.IdAtendimento == atendimento.Id,x=>x.ValorLiquido));

            await _unitOfWorks.SaveChangesAsync();
            await _unitOfWorks.CommitAsync();
            return Unit.Value;
        }
    }
}