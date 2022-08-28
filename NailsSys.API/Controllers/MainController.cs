using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public MainController(IMediator mediator)
        {
            _mediator = mediator;
        }            
    }
}