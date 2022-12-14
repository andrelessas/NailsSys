using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.UsuarioCommands.InserirUsuario;
using NailsSys.Application.Commands.UsuarioCommands.LoginUsuarioCommand;
using NailsSys.Application.Queries.UsuarioQueries.ObterUsuarios;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : MainController
    {
        public UsuarioController(IMediator mediator)
            :base(mediator)
        {
            
        }

        ///<summary>
        ///Obter listagem de usuarios.
        ///</summary>        
        ///<param name = 'page'> página </param>
        [HttpGet]
        // [Authorize(Roles = "Administrador, Gerente")]
        public async Task<IActionResult> ObterUsuariosAsync(int page)
        {
            var usuarios = await _mediator.Send(new ObterUsuariosQueries(page));
            if(usuarios == null)
                return NotFound();
            return Ok(usuarios);
        }

        ///<summary>
        ///Cadastrar usuário.
        ///</summary>        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarUsuarioAsync(InserirUsuarioCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        ///<summary>
        ///Realizar login.
        ///</summary>        
        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> RealizarLoginAsync(LoginUsuarioCommand request)
        {
            var token = await _mediator.Send(request);
            if(token == null)
                return BadRequest();
            
            return Ok(token);
        }
    }
}