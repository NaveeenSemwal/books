using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.API.Services
{
    public interface IBooksServive
    {
        Task<IEnumerable<Models.Book>> GetBooksAsync();

        Task<Models.Book> GetBookAsync(Guid id);

        Task<Guid> AddBook(Models.BookForCreation bookToAdd);
    }
}
