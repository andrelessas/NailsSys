using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Data.Repository;
using NailsSys.Infrastructure.Context;

namespace NailsSys.Infrastructure.Persistense.Repositories
{
    public class AtendimentoRepository : Repository<Atendimento>, IAtendimentoRepository
    {
        public AtendimentoRepository(NailsSysContext context)
            : base(context)
        {
            
        }

        public async Task<IEnumerable<Atendimento>> ObterAtendimentosPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            return await _context.Atendimento.Where(x=>x.DataAtendimento >= dataInicial && x.DataAtendimento <= dataFinal)
                .AsNoTracking()
                .Include(c=>c.Cliente)
                .Include(f=>f.FormaPagamento)
                .Include(i=>i.ItensAtendimento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Atendimento>> ObterAtendimentosRealizadosHoje()
        {
            return await _context.Atendimento
                .Include(c=>c.Cliente)
                .Include(f=>f.FormaPagamento)
                .Include(i=>i.ItensAtendimento)
                .AsNoTracking()
                .Where(x=>x.DataAtendimento.Date == DateTime.Now.Date)
                .ToListAsync();
        }

        public async Task<Atendimento> ObterPorId(int id)
        {
            return await _context.Atendimento.Where(x=>x.Id == id)
                .AsNoTracking()
                .Include(c=>c.Cliente)
                .Include(f=>f.FormaPagamento)
                .Include(i=>i.ItensAtendimento)
                .FirstOrDefaultAsync();    
        }
    }
}