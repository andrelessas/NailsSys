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

        [HttpGet]
        public async Task<IActionResult> ObterTodosItens(int idAgendamento, int page)
        {
            var request = new ObterItensQueries(idAgendamento,page);
            var itens = await _mediator.Send(request);
            if (itens == null)
                return NotFound();
            return Ok(itens);
        }

        [HttpGet("porid")]
        public async Task<IActionResult> ObterItemPorId(int idAgendamento)
        {
            var request = new ObterItemPorIdQueries(idAgendamento);
            var itensAgendamento = await _mediator.Send(request);
            if(itensAgendamento == null)
                return NotFound();
            return Ok(itensAgendamento);
        }

        [HttpPost]
        public async Task<IActionResult> InserirItem(InserirItemAgendamentoCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoverItem(int id)
        {
            await _mediator.Send(new RemoverItemAgendamentoCommand(id));
            return Ok();
        }
    }
}