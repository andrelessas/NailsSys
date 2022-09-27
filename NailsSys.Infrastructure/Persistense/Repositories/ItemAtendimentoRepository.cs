using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Data.Repository;
using NailsSys.Infrastructure.Context;

namespace NailsSys.Infrastructure.Persistense.Repositories
{
    public class ItemAtendimentoRepository:Repository<ItemAtendimento>,IItemAtendimentoRepository
    {
        public ItemAtendimentoRepository(NailsSysContext context)
            :base(context)
        {
            
        }
    }
}