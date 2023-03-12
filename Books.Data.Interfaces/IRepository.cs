using Books.Core.Helpers;
using System.Linq.Expressions;

namespace Books.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<PagedList<TEntity>> GetAllAsync(QueryParams searchParams, Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool traked, string includeProperties = null);

        Task<TEntity> GetAsync(object id);

        void AddAsync(TEntity entity);
        void UpdateAsync(TEntity entity);
        void RemoveAsync(TEntity entity);
        void RemoveAsync(object Id);
        Task<IEnumerable<TEntity>> ExecWithStoreProcedureAsync(string query, params object[] parameters);
    }
}
