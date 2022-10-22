using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.ItemAgendamentoCommands.InserirItem;
using NailsSys.Application.Commands.ItemAgendamentoCommands.RemoverItem;
using NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItemPorId;
using NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItens;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemAgendamentoController : MainController
    {
        public ItemAgendamentoController(IMediator mediator)
            :base(mediator)
        {}

        ///<summary>
        ///Obter itens do agendamento.
        ///</summary>        
        ///<param name = 'idAgendamento'> id do agendamento </param>
        ///<param name = 'page'> página </param>
        [HttpGet]
        public async Task<IActionResult> ObterTodosItens(int idAgendamento, int page)
        {
            var request = new ObterItensAgendamentoQueries(idAgendamento,page);
            var itens = await _mediator.Send(request);
            if (itens == null)
                return NotFound();
            return Ok(itens);
        }

        // ///<summary>
        // ///Obter 
        // ///</summary>        
        // ///<param name = 'data'> data do atendimento </param>
        // [HttpGet("porid")]
        // public async Task<IActionResult> ObterItemPorId(int idAgendamento)
        // {
        //     var request = new ObterItemPorIdQueries(idAgendamento);
        //     var itensAgendamento = await _mediator.Send(request);
        //     if(itensAgendamento == null)
        //         return NotFound();
        //     return Ok(itensAgendamento);
        // }

        ///<summary>
        ///Inserir item no agendamento.
        ///</summary>        
        [HttpPost]
        public async Task<IActionResult> InserirItem(InserirItemAgendamentoCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        ///<summary>
        ///Remover item do agendamento.
        ///</summary>        
        ///<param name = 'id'> id item </param>
        [HttpDelete]
        public async Task<IActionResult> RemoverItem(int id)
        {
            await _mediator.Send(new RemoverItemAgendamentoCommand(id));
            return Ok();
        }
    }
}