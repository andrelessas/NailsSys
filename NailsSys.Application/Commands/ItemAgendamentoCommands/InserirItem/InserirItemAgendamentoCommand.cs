using MediatR;

namespace NailsSys.Application.Commands.ItemAgendamentoCommands.InserirItem
{
    public class InserirItemAgendamentoCommand:IRequest<Unit>
    {
        public int IdProduto { get; set; }
        public int IdAgendamento { get; set; }
        public int Quantidade { get; set; }
    }
}