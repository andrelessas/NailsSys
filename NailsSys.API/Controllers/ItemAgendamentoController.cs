using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NailsSys.Application.InputModels;
using NailsSys.Application.Services.Interfaces;

namespace NailsSys.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemAgendamentoController : MainController
    {
        private readonly IItemAgendamentoService _service;

        public ItemAgendamentoController(IItemAgendamentoService service,IMediator mediator)
            :base(mediator)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Obter(int idAgendamento)
        {
            var itensAgendamento =_service.ObterItens(idAgendamento);
            if(itensAgendamento == null)
                return NotFound();
            return Ok(itensAgendamento);
        }

        [HttpPost]
        public IActionResult Inserir(NovoItemAgendamentoInputModel inputModel)
        {
            _service.InserirItem(inputModel);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            _service.RemoverItem(id);
            return Ok();
        }
    }
}