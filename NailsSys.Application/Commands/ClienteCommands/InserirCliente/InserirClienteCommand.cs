using MediatR;

namespace NailsSys.Application.Commands.ClienteCommands.InserirCliente
{
    public class InserirClienteCommand:IRequest<Unit>
    {
        public string NomeCliente { get; set; }
        public string Telefone { get; set; }
    }
}