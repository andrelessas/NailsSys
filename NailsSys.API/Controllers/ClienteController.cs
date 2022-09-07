using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.ClienteCommands.AlterarCliente;
using NailsSys.Application.Commands.ClienteCommands.BloquearCliente;
using NailsSys.Application.Commands.ClienteCommands.InserirCliente;
using NailsSys.Application.Queries.ClienteQueries.ObterClientePorId;
using NailsSys.Application.Queries.ClienteQueries.ObterClientes;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class ClienteController : MainController
    {
        public ClienteController(IMediator mediator)
            :base(mediator)
        {}  

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
            var cliente = await _mediator.Send(new ObterClientePorIdQueries(id));
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
        [Authorize(Roles = "administrador, gerente")]
        public async Task<IActionResult> BloquearCliente(int id)
        {            
            await _mediator.Send(new BloquearClienteCommand(id));
            
            return Ok();
        }
        
    }
}