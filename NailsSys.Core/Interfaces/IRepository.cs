using System.Linq.Expressions;
using NailsSys.Core.Models;

namespace NailsSys.Core.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> ObterPorIDAsync(int Id);
        Task<PaginationResult<TEntity>> ObterAsync(int page, Expression<Func<TEntity, bool>> predicate);
        Task<PaginationResult<TEntity>> ObterTodosAsync(int page);
        void InserirAsync(TEntity entity);
        void AlterarAsync(TEntity entity);
        void Property(TEntity entity, Expression<Func<TEntity, object>> predicate, bool isModified);
        void ExcluirAsync(TEntity entity);
    }
}