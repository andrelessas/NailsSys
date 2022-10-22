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

        ///<summary>
        ///Obter listagem de clientes
        ///</summary>        
        ///<param name = 'page'> página </param>
        [HttpGet]
        public async Task<IActionResult> ObterClientes(int page)
        {
            var clientes = await _mediator.Send(new ObterClientesQueries(page));
            if (clientes == null)
                return NotFound();

            return Ok(clientes);
        }

        ///<summary>
        ///Obter cliente por id
        ///</summary>        
        ///<param name = 'id'> id cliente </param>
        [HttpGet("porId")]
        public async Task<IActionResult> ObterClientePorId(int id)
        {            
            var cliente = await _mediator.Send(new ObterClientePorIdQueries(id));
            if (cliente == null)
                return NotFound();
                
            return Ok(cliente);
        }

        ///<summary>
        ///Cadastrar cliente.
        ///</summary>        
        [HttpPost]
        public async Task<IActionResult> InserirCliente(InserirClienteCommand request)
        {
            await _mediator.Send(request);            
            return Ok();
        }
        
        ///<summary>
        ///Alterar cadastro de cliente.
        ///</summary>        
        [HttpPut]
        public async Task<IActionResult> AlterarCliente(AlterarClienteCommand request)
        {
            await _mediator.Send(request);
            
            return Ok();
        }
        
        ///<summary>
        ///Bloquear cliente.
        ///</summary>        
        ///<param name = 'id'> id cliente </param>
        [HttpPut("bloquearcliente")]
        [Authorize(Roles = "administrador, gerente")]
        public async Task<IActionResult> BloquearCliente(int id)
        {            
            await _mediator.Send(new BloquearClienteCommand(id));
            
            return Ok();
        }
        
    }
}