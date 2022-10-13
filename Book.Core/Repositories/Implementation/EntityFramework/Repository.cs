using Books.API.Contexts;
using Books.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Books.Core.Repositories.Implementation.EntityFramework
{
    /// <summary>
    /// https://elegantcode.com/2009/12/15/entity-framework-ef4-generic-repository-and-unit-of-work-prototype/
    /// https://www.techpointfunda.com/2021/01/idisposable-interface-and-dispose-method.html
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected BookContext _dbContext;
        protected internal ILogger logger;

        public Repository(BookContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            this.logger = logger;

            logger.LogInformation($"This is from Repository {nameof(logger)}");
        }
        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            try
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public virtual async Task RemoveAsync(object Id)
        {
            TEntity entity = _dbContext.Set<TEntity>().Find(Id);

            if (entity != null)
                _dbContext.Set<TEntity>().Remove(entity);

            await SaveAsync();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool traked)
        {
            if (filter == null)
            {
                throw new ArgumentNullException($"filter must not be null");
            }

            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (!traked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.SingleOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            _dbContext.Set<TEntity>().Remove(entity);
            await SaveAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            _dbContext.Set<TEntity>().Update(entity);
            await SaveAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> ExecWithStoreProcedureAsync(string query, params object[] parameters)
        {
            return await _dbContext.Set<TEntity>().FromSqlRaw(query, parameters).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
