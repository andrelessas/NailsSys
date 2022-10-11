using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NailsSys.Core.DTOs;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Data.Repository;
using NailsSys.Infrastructure.Context;
using NailsSys.Infrastructure.Persistense.Extensions;

namespace NailsSys.Infrastructure.Persistense.Repositories
{
    public class ItemAgendamentoRepository : Repository<ItemAgendamento>, IItemAgendamentoRepository
    {
        private const int PAGE_SIZE = 5;
        public ItemAgendamentoRepository(NailsSysContext context)
            : base(context)
        {

        }

        public async Task InserirItemAsync(ItemAgendamento itemAgendamento)
        {
            await _context.ItemAgendamento.AddAsync(itemAgendamento);
            var produto = await _context.Produto.FindAsync(itemAgendamento.IdProduto);
            itemAgendamento.AtualizarPreco(produto.Preco);
        }

        public async Task<ItemAgendamentoDTO> ObterItemPorId(int idAgendamento)
        {
            var consulta = from i in _context.ItemAgendamento
                           join p in _context.Produto on i.IdProduto equals p.Id
                           where (i.IdAgendamento == idAgendamento)
                           select new ItemAgendamentoDTO(i.Id, i.IdAgendamento, p.Id, p.Descricao, i.Quantidade, i.PrecoInicial, i.Item);

            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<PaginationResult<ItemAgendamentoDTO>> ObterItensAsync(int idAgendamento, int page)
        {
            var consulta = from i in _context.ItemAgendamento
                           join p in _context.Produto on i.IdProduto equals p.Id
                           where (i.IdAgendamento == idAgendamento)
                           select new ItemAgendamentoDTO(i.Id, i.IdAgendamento, p.Id, p.Descricao, i.Quantidade, i.PrecoInicial, i.Item);

            return await consulta.GetPagination<ItemAgendamentoDTO>(page, PAGE_SIZE);
        }

        public async Task<IEnumerable<ItemAgendamento>> ObterItensAsync(int idAgendamento)
        {
            return await _context.ItemAgendamento.Where(x => x.IdAgendamento == idAgendamento).ToListAsync();
        }

        public async Task<int> ObterMaxItem(int idAgendamento)
        {
            return await _context.ItemAgendamento.Where(x => x.IdAgendamento == idAgendamento).Select(i => i.Item).DefaultIfEmpty().MaxAsync();
        }
    }
}