using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.FormaPagamentoCommands.AlterarFormaPagamento;
using NailsSys.Application.Commands.FormaPagamentoCommands.DescontinuarFormaPagamento;
using NailsSys.Application.Commands.FormaPagamentoCommands.InserirFormaPagamento;
using NailsSys.Application.Queries.FormaPagamentoQueries.ObterFormaPagamentoPorId;
using NailsSys.Application.Queries.FormaPagamentoQueries.ObterFormasPagamentos;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormaPagamentoController : MainController
    {
        public FormaPagamentoController(IMediator mediator)
            :base(mediator)
        {
            
        }

        ///<summary>
        ///Obter Listagem de Formas de Pagamentos.
        ///</summary>
        ///<param name = 'page'> Página que será retornada no Get </param>        
        [HttpGet]
        public async Task<IActionResult> ObterAsync(int page)
        {
            var formaPagamento = await _mediator.Send(new ObterFormasPagamentoQueries(page));
            if(formaPagamento == null)
                return NotFound();
            return Ok(formaPagamento);
        }

        ///<summary>
        ///Obter Forma de pagamento por Id
        ///</summary>
        ///<param name = 'id'> Id da forma de pagamento. </param>
        [HttpGet("porId")]
        public async Task<IActionResult> ObterPorIdAsync(int id)
        {
            var formaPagamento = await _mediator.Send(new ObterFormaPagamentoPorIdQueries(id));
            if(formaPagamento == null)
                return NotFound();
            return Ok(formaPagamento);
        }

        ///<summary>
        ///Cadastra uma nova forma de pagamento.
        ///</summary>
        [HttpPost]
        public async Task<IActionResult> InserirAsync(InserirFormaPagamentoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Alterar forma de pagamento.
        ///</summary>
        [HttpPut]
        public async Task<IActionResult> AlterarAsync(AlterarFormaPagamentoCommand inputModel)
        {
            await _mediator.Send(inputModel);
            return Ok();
        }

        ///<summary>
        ///Descontinuar forma de pagamento.
        ///</summary>
        ///<param name = 'id'> Id da forma de pagamento que será descontinuada. </param>
        [HttpPut("descontinuarFormaPagamento")]
        public async Task<IActionResult> DescontinuarFormaPagamentoAsync(int id)
        {
            await _mediator.Send(new DescontinuarFormaPagamentoCommand(id));
            return Ok();
        }
    }
}