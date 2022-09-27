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

        public async Task<int> ObterUltimoIdAtendimento()
        {
            return await _context.Agendamento.Select(x => x.Id).DefaultIfEmpty().MaxAsync();
        }
    }
}