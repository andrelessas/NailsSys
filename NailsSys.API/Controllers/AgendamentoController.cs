using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.InputModels;
using NailsSys.Application.Services.Interfaces;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : MainController
    {
        private readonly IAgendamentoService _service;

        public AgendamentoController(IAgendamentoService service,IMediator mediator)
            :base(mediator)
        {
            _service = service;
        }

        [HttpGet("hoje")]
        public IActionResult ObterAgendamentosDeHoje()
        {
            var agendamentosHoje = _service.ObterUnhasAgendadasHoje();
            if(agendamentosHoje == null)
                return NotFound();
            return Ok(agendamentosHoje);
        }

        [HttpGet("pordata")]
        public IActionResult ObterPorData(DateTime data)
        {
            var agendamentosPorData = _service.ObterUnhasAgendadasPorData(data);
            if(agendamentosPorData == null)
                return NotFound();
            return Ok(agendamentosPorData);    
        }

        [HttpGet("porperiodo")]
        public IActionResult ObterPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            var agendamentosPorPeriodo = _service.ObterUnhasAgendadasPorPeriodoDoDia(dataInicial,dataFinal);
            if(agendamentosPorPeriodo == null)
                return NotFound();
            return Ok(agendamentosPorPeriodo);    
        }

        [HttpPost]
        public IActionResult Inserir(NovoAgendamentoInputModel inputModel)
        {
            _service.NovoAgendamento(inputModel);
            return Ok();
        }

        [HttpPut]
        public IActionResult Alterar(AlterarAgendamentoInputModel inputModel)
        {
            _service.AlterarAgendamento(inputModel);
            return Ok();
        }

        [HttpPut("cancelaragendamento")]
        public IActionResult CancelarAgendamento(int id)
        {
            _service.CancelarAgendamento(id);
            return Ok();
        }
    }
}