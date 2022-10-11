using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Infrastructure.Context;
using NailsSys.Infrastructure.Persistense.Extensions;

namespace NailsSys.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly NailsSysContext _context;
        private const int PAGE_SIZE = 10;
        public Repository(NailsSysContext context)
        {
            _context = context;
        }

        public async Task<PaginationResult<TEntity>> ObterAsync(int page, Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AsNoTracking().Where(predicate).GetPagination<TEntity>(page,PAGE_SIZE);
        }
        public async Task<IEnumerable<TEntity>> ObterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> ObterPorIDAsync(int Id)
        {
            return await _context.Set<TEntity>().FindAsync(Id);
        }

        public async Task<PaginationResult<TEntity>> ObterTodosAsync(int page)
        {
            return await _context.Set<TEntity>().GetPagination<TEntity>(page, PAGE_SIZE);
        }
        public void AlterarAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void ExcluirAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void InserirAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Property(TEntity entity, Expression<Func<TEntity, object>> predicate, bool isModified)
        {
            _context.Entry(entity).Property(predicate).IsModified = isModified;
        }

        public async Task<int> MaxAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity,int>> selected)
        {
            return await _context.Set<TEntity>().Where(predicate).Select(selected).DefaultIfEmpty().MaxAsync();
        }

        public async Task<int> MaxAsync(Expression<Func<TEntity, int>> selected)
        {
            return await _context.Set<TEntity>().Select(selected).DefaultIfEmpty().MaxAsync();
        }

        public async Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity,decimal>> sumValue)
        {
            return await _context.Set<TEntity>().Where(predicate).SumAsync(sumValue);
        }
    }
}