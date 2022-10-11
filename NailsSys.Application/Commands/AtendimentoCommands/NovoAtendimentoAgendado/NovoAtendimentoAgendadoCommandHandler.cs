using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.AtendimentoCommands.NovoAtendimentoAgendado
{
    public class NovoAtendimentoAgendadoCommandHandler : IRequestHandler<NovoAtendimentoAgendadoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public NovoAtendimentoAgendadoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(NovoAtendimentoAgendadoCommand request, CancellationToken cancellationToken)
        {
            var agendamento = await _unitOfWorks.Agendamento.ObterPorIDAsync(request.IdAgendamento);
            if (agendamento == null)
                throw new ExcecoesPersonalizadas("O agendamento informado não existe.");

            var itensAgendamento = await _unitOfWorks.ItemAgendamento.ObterItensAsync(request.IdAgendamento);
            if (!itensAgendamento.Any())
                throw new ExcecoesPersonalizadas("O agendamento informado não possui itens lançados.");

            await _unitOfWorks.BeginTransactionAsync();
            _unitOfWorks.Atendimento.InserirAsync(new Atendimento(
                agendamento.IdCliente,
                agendamento.DataAtendimento,
                request.IdFormaPagamento,
                (request.InicioAtendimento != null ? request.InicioAtendimento.GetValueOrDefault() : agendamento.InicioPrevisto),
                (request.TerminoAtendimento != null ? request.TerminoAtendimento.GetValueOrDefault() : agendamento.TerminoPrevisto)));

            var maxIdAtendimento = await _unitOfWorks.Atendimento.MaxAsync(x => x.Id);

            foreach (var item in itensAgendamento)
            {
                _unitOfWorks.ItemAtendimento.InserirAsync(new ItemAtendimento(
                    maxIdAtendimento+1,
                    item.IdProduto,
                    item.Quantidade,
                    item.Item));
            }

            await _unitOfWorks.SaveChangesAsync();
            await _unitOfWorks.CommitAsync();
            return Unit.Value;
        }
    }
}