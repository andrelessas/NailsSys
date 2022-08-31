using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosHoje
{
    public class ObterAgendamentosHojeQueriesHandler : IRequestHandler<ObterAgendamentosHojeQueries, IEnumerable<AgendamentoViewModel>>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;

        public ObterAgendamentosHojeQueriesHandler(IAgendamentoRepository agendamentoRepository,
                                                     IItemAgendamentoRepository itemAgendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _itemAgendamentoRepository = itemAgendamentoRepository;
        }
        public async Task<IEnumerable<AgendamentoViewModel>> Handle(ObterAgendamentosHojeQueries request, CancellationToken cancellationToken)
        {
            var lstItens = new List<ItemAgendamentoViewModel>();
            var lstAgendamentos = new List<AgendamentoViewModel>();

            var agendamentos = await _agendamentoRepository.ObterAgendamentosHojeAsync();        
            
            lstAgendamentos = agendamentos.ToList().ConvertAll(a => new AgendamentoViewModel(a.DataAtendimento,
                                                                                             a.InicioPrevisto,
                                                                                             a.TerminoPrevisto,
                                                                                             a.Cliente.NomeCliente,
                                                                                             a.Id));
                                                                                             
            foreach (var agendamento in lstAgendamentos)
            {
                var itensDTO = await _itemAgendamentoRepository.ObterItensAsync(agendamento.Id);                
                agendamento.Itens = itensDTO.ToList().ConvertAll(i => new ItemAgendamentoViewModel(i.DescricaoProduto,i.Quantidade,i.PrecoInicial,i.Id));                
            }

            return lstAgendamentos;
        }
    }
}