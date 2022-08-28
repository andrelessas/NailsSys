using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Application.InputModels;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Services.Interfaces
{
    public interface IItemAgendamentoService
    {
        List<ItemAgendamentoViewModel> ObterItens(int idAgendamento);
        void InserirItem(NovoItemAgendamentoInputModel inpuModel);
        void AlterarItem(AlterarItemAgendamentoInputModel inpuModel);
        void RemoverItem(int id);
    }
}