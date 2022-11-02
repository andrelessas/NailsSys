using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        ///<summary>
        ///Obter listagem de produtos.
        ///</summary>        
        ///<param name = 'page'> página </param>
        [HttpGet]
        public async Task<IActionResult> ObterProdutos(int page)
        {
            var query = new ObterProdutosQueries(page);
            var produtos = await _mediator.Send(query);
            if(produtos == null)
                return NotFound();
            
            return Ok(produtos);
        }

        ///<summary>
        ///Obter produto por id.
        ///</summary>        
        ///<param name = 'id'> id do produto </param>
        [HttpGet("porid")]
        public async Task<IActionResult> ObterProdutoPorId(int id)
        {
            var produto = await _mediator.Send(new ObterProdutoPorIdQueries(id));
            if(produto == null)
                return NotFound();
            
            return Ok(produto);
        }

        ///<summary>
        ///Cadastrar produto.
        ///</summary>        
        [HttpPost]
        // [Authorize(Roles = "administrador, gerente")]
        public async Task<IActionResult> InserirProduto(InserirProdutoCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        ///<summary>
        ///Alterar cadastro do produto.
        ///</summary>        
        [HttpPut]
        [Authorize(Roles = "administrador, gerente")]
        public async Task<IActionResult> AlterarProduto(AlterarProdutoCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        ///<summary>
        ///Descontinuar produto.
        ///</summary>        
        ///<param name = 'id'> id do produto </param>
        [HttpPut("descontinuar")]
        [Authorize(Roles = "administrador, gerente")]
        public async Task<IActionResult> DescontinuarProduto(int id)
        {
            await _mediator.Send(new DescontinuarProdutoCommand(id));
            return Ok();
        }
    }
}