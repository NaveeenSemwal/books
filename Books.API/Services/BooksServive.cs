using AutoMapper;
using Books.API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Books.API.Services
{
    public class BooksServive : IBooksServive
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public BooksServive(IBooksRepository booksRepository, IMapper mapper)
        {
            _booksRepository = booksRepository;

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Guid> AddBook(BookForCreation bookForCreation)
        {
            var bookEntity = _mapper.Map<Entities.Book>(bookForCreation);

            _booksRepository.AddBook(bookEntity);

            await _booksRepository.SaveChangesAsync();

            return bookEntity.Id;
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            var bookEntity = await _booksRepository.GetBookAsync(id);

            return _mapper.Map<Book>(bookEntity);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var booksEntity = await _booksRepository.GetBooksAsync();

            return _mapper.Map<IEnumerable<Book>>(booksEntity);
        }

        public async Task UpdateBookPatchAsync(Guid bookId, JsonPatchDocument<BookForCreation> model)
        {
            var bookEntity = _mapper.Map<JsonPatchDocument<Entities.Book>>(model);

            await _booksRepository.UpdateBookPatch(bookId, bookEntity);

            await _booksRepository.SaveChangesAsync();
        }
    }
}
