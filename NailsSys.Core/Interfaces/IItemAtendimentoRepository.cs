using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Entities;

namespace NailsSys.Core.Interfaces
{
    public interface IItemAtendimentoRepository:IRepository<ItemAtendimento>
    {
        Task InserirItemAtendimento(ItemAtendimento itemAtendimento);
        Task<ItemAtendimento> ObterItemAtendimento(int item, int idAtendimento);
    }
}