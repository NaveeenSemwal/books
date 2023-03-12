using Books.Data.Model;

namespace Books.Data.Dapper
{
    public class UserDapperRepository
    {
        private readonly DapperContext _context;
        public UserDapperRepository(DapperContext context)
        {
            _context = context;
        }


        public Task<(ApplicationUser LocalUser, string Token)> Login(ApplicationUser localUser, string password)
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
