using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.AgendamentoCommands.AlterarAgendamento;
using NailsSys.Application.Commands.AgendamentoCommands.NovoAgendamento;
using NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosHoje;
using NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorData;
using NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosPorPeriodoDoDia;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : MainController
    {
        public AgendamentoController(IMediator mediator)
            :base(mediator)
        {}

        [HttpGet("hoje")]        
        public async Task<IActionResult> ObterAgendamentosDeHoje()
        {
            var agendamentosHoje = await _mediator.Send(new ObterAgendamentosHojeQueries());
            if(agendamentosHoje == null)
                return NotFound();
            return Ok(agendamentosHoje);
        }

        [HttpGet("pordata")]        
        public async Task<IActionResult> ObterPorData(DateTime data)
        {
            var request = new ObterAgendamentosPorDataQueries(data);
            var agendamentosPorData = await _mediator.Send(request);
            if(agendamentosPorData == null)
                return NotFound();
            return Ok(agendamentosPorData);    
        }

        [HttpGet("porperiodo")]
        public async Task<IActionResult> ObterAgenamentoPorPeriodoDia(DateTime dataInicial, DateTime dataFinal)
        {
            var request = new ObterAgendamentosPorPeriodoDoDiaQueries(dataInicial,dataFinal);
            var agendamentosPorPeriodo = await _mediator.Send(request);
            if(agendamentosPorPeriodo == null)
                return NotFound();
            return Ok(agendamentosPorPeriodo);    
        }

        [HttpPost]
        public async Task<IActionResult> NovoAgendamento(NovoAgendamentoCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> AlterarAgendamento(AlterarAgendamentoCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPut("cancelaragendamento")]
        [Authorize(Roles = "administrador, gerente")]
        public async Task<IActionResult> CancelarAgendamento(int id)
        {
            await _mediator.Send(id);
            return Ok();
        }
    }
}