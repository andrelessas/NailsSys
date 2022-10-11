using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Commands.AtendimentoCommands.AlterarAtendimento
{
    public class AlterarAtendimentoCommandHandler : IRequestHandler<AlterarAtendimentoCommand, Unit>
    {
        private readonly IUnitOfWorks _unitOfWorks;

        public AlterarAtendimentoCommandHandler(IUnitOfWorks unitOfWorks)
        {
            _unitOfWorks = unitOfWorks;
        }
        public async Task<Unit> Handle(AlterarAtendimentoCommand request, CancellationToken cancellationToken)
        {
            var atendimento = await _unitOfWorks.Atendimento.ObterPorIDAsync(request.IdAtendimento);
            if(atendimento == null)
                throw new ExcecoesPersonalizadas("Atendimento não encontrado.");
            
            atendimento.AlterarAtendimento(
                request.IdFormaPagamento,
                request.IdCliente,
                request.DataAtendimento,
                request.InicioAtendimento,
                request.TerminoAtendimento);     
                       
            await _unitOfWorks.SaveChangesAsync();            
            return Unit.Value;
        }
    }
}