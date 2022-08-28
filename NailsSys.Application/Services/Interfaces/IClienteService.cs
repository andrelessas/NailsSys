using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Application.InputModels;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Services.Interfaces
{
    public interface IClienteService
    {
        List<ClienteViewModel> ObterTodosClientes();
        ClienteViewModel ObterClientePorId(int id);
        void InserirCliente(NovoClienteInputModel inputModel);
        void AlterarCliente(AlterarClienteInputModel inputModel);
        void BloquearCliente(int id);
    }
}