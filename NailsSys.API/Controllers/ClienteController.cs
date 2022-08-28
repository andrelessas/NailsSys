using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.ClienteCommands.AlterarCliente;
using NailsSys.Application.Commands.ClienteCommands.InserirCliente;
using NailsSys.Application.InputModels;
using NailsSys.Application.Queries.ClienteQueries.ObterClientes;
using NailsSys.Application.Services.Interfaces;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : MainController
    {
        // private readonly IMediator _mediator;
        public ClienteController(IMediator mediator)
            :base(mediator)
        {
            // _mediator = mediator;
        }  

        [HttpGet]
        public async Task<IActionResult> ObterClientes()
        {
            var clientes = await _mediator.Send(new ObterClientesQueries());
            if (clientes == null)
                return NotFound();

            return Ok(clientes);
        }

        [HttpGet("porId")]
        public async Task<IActionResult> ObterClientePorId(int id)
        {
            var cliente = await _mediator.Send(id);
            if (cliente == null)
                return NotFound();
                
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> InserirCliente(InserirClienteCommand request)
        {
            await _mediator.Send(request);            
            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> AlterarCliente(AlterarClienteCommand request)
        {
            await _mediator.Send(request);
            
            return Ok();
        }
        
        [HttpPut("bloquearcliente")]
        public async Task<IActionResult> BloquearCliente(int id)
        {
            await _mediator.Send(id);
            
            return Ok();
        }
        
    }
}