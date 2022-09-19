using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class MainController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public MainController(IMediator mediator)
        {
            _mediator = mediator;
        }            
    }
}