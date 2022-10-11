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
    public class ItemAtendimentoRepository : Repository<ItemAtendimento>, IItemAtendimentoRepository
    {
        public ItemAtendimentoRepository(NailsSysContext context)
            : base(context)
        {

        }

        public async Task InserirItemAtendimento(ItemAtendimento itemAtendimento)
        {
            await _context.ItemAtendimento.AddAsync(itemAtendimento);
            var produto = await _context.Produto.FindAsync(itemAtendimento.IdProduto);
            itemAtendimento.AtualizarPreco(produto.Preco);
        }

        public async Task<ItemAtendimento> ObterItemAtendimento(int item, int idAtendimento)
        {
            return await _context.ItemAtendimento.FirstOrDefaultAsync(x=>x.Item == item && x.IdAtendimento == idAtendimento);
        }
    }
}