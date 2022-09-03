using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.ProdutoCommands.AlterarProduto;
using NailsSys.Application.Commands.ProdutoCommands.DescontinuarProduto;
using NailsSys.Application.Commands.ProdutoCommands.InserirProduto;
using NailsSys.Application.Queries.ProdutoQueries.ObterProdutoPorId;
using NailsSys.Application.Queries.ProdutoQueries.ObterProdutos;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : MainController
    {
        public ProdutoController(IMediator mediator)
            :base(mediator)
        {}

        [HttpGet]
        public async Task<IActionResult> ObterProdutos()
        {
            var query = new ObterProdutosQueries();
            var produtos = await _mediator.Send(query);
            if(produtos == null)
                return NotFound();
            
            return Ok(produtos);
        }
        [HttpGet("porid")]
        public async Task<IActionResult> ObterProdutoPorId(int id)
        {
            var produto = await _mediator.Send(new ObterProdutoPorIdQueries(id));
            if(produto == null)
                return NotFound();
            
            return Ok(produto);
        }
        [HttpPost]
        public async Task<IActionResult> InserirProduto(InserirProdutoCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> AlterarProduto(AlterarProdutoCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPut("descontinuar")]
        public async Task<IActionResult> DescontinuarProduto(int id)
        {
            await _mediator.Send(new DescontinuarProdutoCommand(id));
            return Ok();
        }
    }
}