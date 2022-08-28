using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Application.InputModels;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Services.Interfaces
{
    public interface IAgendamentoService
    {
        List<AgendamentoViewModel> ObterUnhasAgendadasHoje();
        List<AgendamentoViewModel> ObterUnhasAgendadasPorData(DateTime data);
        List<AgendamentoViewModel> ObterUnhasAgendadasPorPeriodoDoDia(DateTime horarioInicial,DateTime horarioFinal);
        void NovoAgendamento(NovoAgendamentoInputModel inputModel);
        void AlterarAgendamento(AlterarAgendamentoInputModel inputModel);
        void CancelarAgendamento(int id);
    }
}