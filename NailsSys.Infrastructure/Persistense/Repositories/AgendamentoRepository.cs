
using Microsoft.EntityFrameworkCore;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Data.Repository;
using NailsSys.Infrastructure.Context;

namespace NailsSys.Infrastructure.Persistense.Repositories
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(NailsSysContext context)
            :base(context)
        {}
        public async Task<IEnumerable<Agendamento>> ObterAgendamentosHojeAsync()
        {
            return await _context.Agendamento.Include(c=>c.Cliente)
                                             .Where(d=> d.DataAtendimento.Date == DateTime.Now.Date)
                                             .ToListAsync();
        }

        public async Task<IEnumerable<Agendamento>> ObterAgendamentosPorDataAsync(DateTime data)
        {
            return await _context.Agendamento.Include(c=>c.Cliente)
                                             .Where(d => d.DataAtendimento.Date == data.Date)
                                             .ToListAsync();
        }

        public async Task<IEnumerable<Agendamento>> ObterAgendamentosPorPeriodoDoDiaAsync(DateTime horarioInicial, DateTime horarioFinal)
        {
            return await _context.Agendamento.Include(c=>c.Cliente)
                                             .Where(d => d.DataAtendimento.Date == DateTime.Now.Date && 
                                                         d.InicioPrevisto >= horarioInicial &&
                                                         d.TerminoPrevisto <= horarioFinal)
                                             .ToListAsync();
        }

        public async Task<int> ObterMaxAgendamento()
        {
            return await _context.Agendamento.Select(a => a.Id).DefaultIfEmpty().MaxAsync();
        }
    }
}