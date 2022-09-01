using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Commands.AgendamentoCommands.NovoAgendamento
{
    public class NovoAgendamentoCommandHandler : IRequestHandler<NovoAgendamentoCommand, Unit>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public NovoAgendamentoCommandHandler(IAgendamentoRepository agendamentoRepository,
                                             IItemAgendamentoRepository itemAgendamentoRepository,
                                             IProdutoRepository produtoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _itemAgendamentoRepository = itemAgendamentoRepository;
            _produtoRepository = produtoRepository;
        }
        public async Task<Unit> Handle(NovoAgendamentoCommand request, CancellationToken cancellationToken)
        {
            _agendamentoRepository.InserirAsync(new Agendamento(request.idCliente,
                                                                request.DataAtendimento,
                                                                request.InicioPrevisto,
                                                                request.TerminoPrevisto));
            await _agendamentoRepository.SaveChangesAsync();
            
            var maxAgendamento = await _agendamentoRepository.ObterMaxAgendamento();

            foreach (var item in request.Itens)
            {
                var maxItem = await _itemAgendamentoRepository.ObterMaxItem(maxAgendamento);
                await _itemAgendamentoRepository.InserirItemAsync(new ItemAgendamento(maxAgendamento,item.IdProduto,item.Quantidade,maxItem+1));
                await _itemAgendamentoRepository.SaveChangesAsync();
            }
            

            return Unit.Value;
        }
    }
}