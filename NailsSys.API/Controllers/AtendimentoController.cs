using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.AtendimentoCommands.AlterarAtendimento;
using NailsSys.Application.Commands.AtendimentoCommands.CancelarAtendimento;
using NailsSys.Application.Commands.AtendimentoCommands.NovoAtendimento;
using NailsSys.Application.Commands.AtendimentoCommands.NovoAtendimentoAgendado;
using NailsSys.Application.Queries.AtendimentoQueries.ObterAtendimentoPorId;
using NailsSys.Application.Queries.AtendimentoQueries.ObterAtendimentoRealizadosHoje;
using NailsSys.Application.Queries.AtendimentoQueries.ObterAtendimentosPorPeriodo;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtendimentoController : MainController
    {
        public AtendimentoController(IMediator mediator)
            :base(mediator)
        {
            
        }  

        ///<summary>
        ///Obter atendimentos por período.
        ///</summary>
        ///<param name = 'dataInicial'> Data Inicial </param>
        ///<param name = 'dataFinal'> Data Final </param>
        [HttpGet("AtendimentosPorPeriodo")]
        public async Task<IActionResult> ObterAsync(DateTime dataInicial, DateTime dataFinal)
        {
            var atendimentos = await _mediator.Send(new ObterAtendimentosPorPeriodoQueries(dataInicial,dataFinal));
            if(!atendimentos.Any())
                return NotFound();

            return Ok(atendimentos);
        }

        ///<summary>
        ///Obter atendimentos realizados hoje.
        ///</summary>
        [HttpGet("AtendimentosHoje")]
        public async Task<IActionResult> ObterAsync()
        {
            var atendimentos = await _mediator.Send(new ObterAtendimentosRealizadosHojeQueries());
            if(!atendimentos.Any())
                return NotFound();

            return Ok(atendimentos);
        }

        ///<summary>
        ///Obter atendimento por Id
        ///</summary>
        ///<param name = 'id'> Id Atendimento </param>
        [HttpGet("PorId")]
        public async Task<IActionResult> ObterAsync(int id)
        {
            var atendimento = await _mediator.Send(new ObterAtendimentoPorIDQueries(id));
            if(atendimento == null)
                return NotFound();

            return Ok(atendimento);
        }

        ///<summary>
        ///Inserir atendimento sem agendamento lançado.
        ///</summary>
        [HttpPost]
        public async Task<IActionResult> InserirAsync(NovoAtendimentoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Inserir Atendimento através de um agendamento
        ///</summary>
        [HttpPost("AtendimentoAgendado")]
        public async Task<IActionResult> InserirAsync(NovoAtendimentoAgendadoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Alterar atendimento
        ///</summary>
        [HttpPut]
        public async Task<IActionResult> AlterarAsync(AlterarAtendimentoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Cancelar agendamento
        ///</summary>
        [HttpPut("CancelarAtendimento")]
        public async Task<IActionResult> AlterarAsync(CancelarAtendimentoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }
        
    }
}