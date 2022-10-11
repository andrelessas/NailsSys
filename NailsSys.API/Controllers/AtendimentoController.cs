using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.AtendimentoCommands.NovoAtendimento;

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
        ///Inserir atendimento sem agendamento lançado.
        ///</summary>
        [HttpPost]
        public async Task<IActionResult> InserirAsync(NovoAtendimentoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }
    }
}