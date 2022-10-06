using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Books.API.Services
{
    /// <summary>
    /// https://elegantcode.com/2009/12/15/entity-framework-ef4-generic-repository-and-unit-of-work-prototype/
    /// https://www.techpointfunda.com/2021/01/idisposable-interface-and-dispose-method.html
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _dbContext;
        protected internal ILogger logger;

        public Repository(DbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            this.logger = logger;

            logger.LogInformation($"This is from Repository { nameof(logger)}");
        }
        public virtual void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            try
            {
                _dbContext.Set<TEntity>().Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public virtual void Delete(object Id)
        {
            TEntity entity = _dbContext.Set<TEntity>().Find(Id);
            if (entity != null)
                _dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual TEntity Find(object Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException($"{nameof(Id)} Id must not be null");
            }

            return _dbContext.Set<TEntity>().Find(Id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            var data = _dbContext.Set<TEntity>();
            return data;
        }

        public virtual void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            _dbContext.Set<TEntity>().Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }

            _dbContext.Set<TEntity>().Update(entity);
        }

        public virtual IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return _dbContext.Set<TEntity>().FromSqlRaw(query, parameters).ToList();
        }
    }
}
