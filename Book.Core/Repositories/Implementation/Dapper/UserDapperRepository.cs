using Books.API.Entities;
using Books.API.Services.Abstract;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Books.Core.Repositories.Implementation.Dapper
{
    public class UserDapperRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserDapperRepository(DapperContext context)
        {
            _context = context;
        }

        public Task AddAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> ExecWithStoreProcedureAsync(string query, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ApplicationUser>> GetAllAsync(Expression<Func<ApplicationUser, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetAsync(Expression<Func<ApplicationUser, bool>> filter, bool traked)
        {
            throw new NotImplementedException();
        }

        public bool IsUniqueUser(string userName)
        {
            throw new NotImplementedException();
        }

        public  Task<(ApplicationUser LocalUser, string Token)> Login(ApplicationUser localUser, string password)
        {

            throw new NotImplementedException();

            //var sql = "SELECT * FROM Products WHERE Id = @Id";

            //ApplicationUser user = null;
            //using (var connection = _context.CreateConnection)
            //{
            //    connection.Open();
            //    user = await connection.QuerySingleOrDefaultAsync<ApplicationUser>(sql, new { Id = 1 });

            //}
        }

        public Task RemoveAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(object Id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
