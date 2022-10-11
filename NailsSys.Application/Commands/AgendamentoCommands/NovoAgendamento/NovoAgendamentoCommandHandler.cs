using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.AgendamentoCommands.NovoAgendamento
{
    public class NovoAgendamentoCommandHandler : IRequestHandler<NovoAgendamentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public NovoAgendamentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(NovoAgendamentoCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWorks.BeginTransactionAsync();

            _unitOfWorks.Agendamento.InserirAsync(
                new Agendamento(
                    request.IdCliente,
                    request.DataAtendimento,
                    request.InicioPrevisto,
                    request.TerminoPrevisto));

            await _unitOfWorks.SaveChangesAsync();

            if (request.Itens == null || request.Itens.Count() == 0)
                throw new ExcecoesPersonalizadas("Nenhum produto informado para realizar o agendamento.");

            var maxAgendamento = await _unitOfWorks.Agendamento.MaxAsync(x => x.Id);

            foreach (var item in request.Itens)
            {
                var maxItem = await _unitOfWorks.ItemAgendamento.ObterMaxItem(maxAgendamento);
                await _unitOfWorks.ItemAgendamento.InserirItemAsync(
                    new ItemAgendamento(
                        maxAgendamento,
                        item.IdProduto,
                        item.Quantidade,
                        maxItem + 1));

                await _unitOfWorks.SaveChangesAsync();
            }
            await _unitOfWorks.CommitAsync();
            return Unit.Value;
        }
    }
}