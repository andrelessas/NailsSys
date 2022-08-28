using System.Linq.Expressions;

namespace NailsSys.Core.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> ObterPorIDAsync(int Id);
        Task<IEnumerable<TEntity>> ObterAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> ObterTodosAsync();
        void InserirAsync(TEntity entity);
        void AlterarAsync(TEntity entity);
        void Property(TEntity entity, Expression<Func<TEntity, object>> predicate, bool isModified);
        void ExcluirAsync(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}