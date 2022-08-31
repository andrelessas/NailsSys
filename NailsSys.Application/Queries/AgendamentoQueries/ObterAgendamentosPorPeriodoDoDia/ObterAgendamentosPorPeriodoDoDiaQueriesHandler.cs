using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorPeriodoDoDia
{
    public class ObterAgendamentosPorPeriodoDoDiaQueriesHandler : IRequestHandler<ObterAgendamentosPorPeriodoDoDiaQueries, IEnumerable<AgendamentoViewModel>>
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IItemAgendamentoRepository _itemAgendamentoRepository;

        public ObterAgendamentosPorPeriodoDoDiaQueriesHandler(IAgendamentoRepository agendamentoRepository,IItemAgendamentoRepository itemAgendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _itemAgendamentoRepository = itemAgendamentoRepository;   
        }
        public async Task<IEnumerable<AgendamentoViewModel>> Handle(ObterAgendamentosPorPeriodoDoDiaQueries request, CancellationToken cancellationToken)
        {
            var lstItens = new List<ItemAgendamentoViewModel>();
            var lstAgendamentos = new List<AgendamentoViewModel>();

            request.PeriodoInicial = new DateTime(request.PeriodoInicial.Year,request.PeriodoInicial.Month,request.PeriodoInicial.Day,request.PeriodoInicial.Hour,request.PeriodoInicial.Minute,request.PeriodoInicial.Second);
            request.PeriodoFinal = new DateTime(request.PeriodoFinal.Year,request.PeriodoFinal.Month,request.PeriodoFinal.Day,request.PeriodoFinal.Hour,request.PeriodoFinal.Minute,request.PeriodoFinal.Second);

            var agendamentos = await _agendamentoRepository.ObterAgendamentosPorPeriodoDoDiaAsync(request.PeriodoInicial,request.PeriodoFinal);        
            
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