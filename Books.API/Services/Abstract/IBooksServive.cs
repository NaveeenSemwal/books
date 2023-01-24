using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Books.API.Models.Dto;
using Books.Core.Helpers;
using Microsoft.AspNetCore.JsonPatch;

namespace Books.API.Services.Abstract
{
    public interface IBooksServive
    {
        Task<PagedList<Book>> GetBooksAsync(QueryParams searchParams);

        Task<Book> GetBookAsync(Guid id);

        Task<Guid> AddBook(BookForCreation bookToAdd);

        Task UpdateBookPatchAsync(Guid bookId, JsonPatchDocument<BookForCreation> model);
    }
}
