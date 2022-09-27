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
            var agendamento = await _unitOfWorks.Agendamento.ObterPorIDAsync(request.IdAgendamento);
            
            if(agendamento == null)
                throw new ExcecoesPersonalizadas("Agendamento não encontrado.");
            
            await _unitOfWorks.BeginTransactionAsync();

            _unitOfWorks.Atendimento.InserirAsync(new Atendimento(
                agendamento.IdCliente,
                agendamento.DataAtendimento,
                agendamento.AtendimentoRealizado,
                request.IdFormaPagamento,
                agendamento.InicioPrevisto,
                agendamento.TerminoPrevisto));

            await _unitOfWorks.SaveChangesAsync();

            var idAtendimento = await _unitOfWorks.Atendimento.ObterUltimoIdAtendimento();
            foreach (var item in agendamento.ItemAgendamentos)
            {
                _unitOfWorks.ItemAtendimento.InserirAsync(new ItemAtendimento(idAtendimento,item.IdProduto,item.Quantidade));
                await _unitOfWorks.SaveChangesAsync();
            }

            await _unitOfWorks.CommitAsync();

            return Unit.Value;
        }
    }
}