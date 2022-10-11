using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.InputModels;

namespace NailsSys.Application.Commands.AtendimentoCommands.NovoAtendimento
{
    public class NovoAtendimentoCommand:IRequest<Unit>
    {
        public int IdFormaPagamento { get; set; }    
        public int IdCliente { get; set; }
        public DateTime DataAtendimento { get; set; }
        public DateTime InicioAtendimento { get; set; }
        public DateTime TerminoAtendimento { get; set; }
        public bool AtendimentoRealizado { get; set; } 
        public List<ItemInputModel> Itens { get; set; }
    }
}