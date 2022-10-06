using Books.API.Contexts;
using Books.API.Entities;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Services
{
    public class BooksRepository : IBooksRepository, IDisposable
    {
        private BookContext _context;

        public BooksRepository(BookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<Book> GetBookAsync(Guid id)
        {
            return await _context.Books
                .Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public IEnumerable<Book> GetBooks()
        {
            //_context.Database.ExecuteSqlRaw("WAITFOR DELAY '00:00:02';");
            return _context.Books.Include(b => b.Author).ToList();
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            //await _context.Database.ExecuteSqlRawAsync("WAITFOR DELAY '00:00:02';");
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }

            }
        }

        /// <summary>
        ///  Question : Why we have't use AddAsync() to save data asyncronalsy 
        ///  
        /// Answer : Go to the defination of _context.Books.AddAsync().
        /// 
        ///  This method is async only to allow special value generators, such as the one
        //     used by 'Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo',
        //     to access the database asynchronously.
        //
        //     For all other cases the non async method should be used.
        /// </summary>
        /// <param name="bookToAdd"></param>
        public void AddBook(Book bookToAdd)
        {
            if (bookToAdd == null)
            {
                throw new ArgumentNullException(nameof(bookToAdd));
            }
            _context.Books.Add(bookToAdd);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task UpdateBookPatch(Guid bookId, JsonPatchDocument<Entities.Book> bookModel)
        {
            var book = await _context.Books.FindAsync(bookId);

            if (book != null)
            {
                bookModel.ApplyTo(book);

                _context.Books.Update(book);
            }
        }
    }
}
