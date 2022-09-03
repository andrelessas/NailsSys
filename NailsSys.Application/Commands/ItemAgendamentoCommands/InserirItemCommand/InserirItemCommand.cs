using MediatR;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.InserirItemCommand
{
    public class InserirItemCommand:IRequest<Unit>
    {
        public int IdProduto { get; set; }
        public int IdAgendamento { get; set; }
        public int Quantidade { get; set; }
    }
}