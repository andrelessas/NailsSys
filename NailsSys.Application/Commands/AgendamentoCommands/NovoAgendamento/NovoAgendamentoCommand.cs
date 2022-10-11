using MediatR;
using NailsSys.Application.InputModels;

namespace NailsSys.Application.Commands.AgendamentoCommands.NovoAgendamento
{
    public class NovoAgendamentoCommand:IRequest<Unit>
    {
        public int IdCliente { get; set; }
        public DateTime DataAtendimento { get; set; }
        public DateTime InicioPrevisto { get; set; }
        public DateTime TerminoPrevisto { get; set; }
        public List<ItemInputModel> Itens { get; set; }
    }
}