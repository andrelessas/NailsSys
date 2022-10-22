using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.ItemAtendimentoCommands.AlterarItem;
using NailsSys.Application.Commands.ItemAtendimentoCommands.InserirItem;
using NailsSys.Application.Commands.ItemAtendimentoCommands.RemoverItem;

namespace NailsSys.API.Controllers
{
    [ApiController]
    public class ItemAtendimentoController:MainController
    {
        public ItemAtendimentoController(IMediator mediator)
            :base(mediator)
        {}

        ///<summary>
        ///Inserir item atendimento
        ///</summary>
        [HttpPost]
        public async Task<IActionResult> InserirAsync(InserirItemAtendimentoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Alterar item atendimento
        ///</summary>
        [HttpPut]
        public async Task<IActionResult> AlterarAsync(AlterarItemAtendimentoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Remover item atendimento.
        ///</summary>
        ///<param name = 'idAtendimento'> id do atendimento </param>
        ///<param name = 'item'> item atendimento </param>
        [HttpDelete]
        public async Task<IActionResult> ExcluirAsync(int idAtendimento, int item)
        {
            await _mediator.Send(new RemoverItemAtendimentoCommand(idAtendimento,item));
            return Ok();
        }
    }
}