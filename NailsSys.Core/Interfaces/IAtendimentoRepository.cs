using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Entities;

namespace NailsSys.Core.Interfaces
{
    public interface IAtendimentoRepository : IRepository<Atendimento>
    {
        Task<IEnumerable<Atendimento>> ObterAtendimentosPorPeriodo(DateTime dataInicial, DateTime dataFinal);
        Task<IEnumerable<Atendimento>> ObterAtendimentosRealizadosHoje();
        Task<Atendimento> ObterPorId(int id);        
    }
}