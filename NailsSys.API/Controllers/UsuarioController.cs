using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.Commands.UsuarioCommands.InserirUsuario;

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

        [HttpPost]
        public async Task<IActionResult> InserirAsync(InserirUsuarioCommand request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}