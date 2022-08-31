using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Entities;

namespace NailsSys.Core.Interfaces
{
    public interface IAgendamentoRepository:IRepository<Agendamento>
    {
        Task<IEnumerable<Agendamento>> ObterAgendamentosHojeAsync();
        Task<IEnumerable<Agendamento>> ObterAgendamentosPorDataAsync(DateTime data);
        Task<IEnumerable<Agendamento>> ObterAgendamentosPorPeriodoDoDiaAsync(DateTime horarioInicial,DateTime horarioFinal);
    }
}