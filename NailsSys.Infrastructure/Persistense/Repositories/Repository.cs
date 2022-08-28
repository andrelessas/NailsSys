using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NailsSys.Core.Interfaces;
using NailsSys.Infrastructure.Context;

namespace NailsSys.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly NailsSysContext _context;

        public Repository(NailsSysContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> ObterAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> ObterPorIDAsync(int Id)
        {
            return await _context.Set<TEntity>().FindAsync(Id);
        }

        public async Task<IEnumerable<TEntity>> ObterTodosAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
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

        public void Property(TEntity entity,Expression<Func<TEntity, object>> predicate, bool isModified)
        {
            _context.Entry(entity).Property(predicate).IsModified = isModified;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}