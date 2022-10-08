﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.API.Services
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter ,bool traked);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task RemoveAsync(object Id);
        Task<IEnumerable<TEntity>> ExecWithStoreProcedureAsync(string query, params object[] parameters);

        Task SaveAsync();
    }
}