using Books.Data.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace Books.Data.Interfaces
{
    public interface IBooksRepository : IRepository<Book>
    {
        Task UpdateBookPatch(Guid bookId, JsonPatchDocument<Book> model);
    }
}
