﻿using AutoMapper;
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

        /// <summary>
        /// Primary key which is GUID is auto generated. See migration classes - ValueGeneratedOnAdd()
        /// </summary>
        /// <param name="bookForCreation"></param>
        /// <returns></returns>
        public async Task<Guid> AddBook(BookForCreation bookForCreation)
        {
            var bookEntity = _mapper.Map<Entities.Book>(bookForCreation);

            await _booksRepository.AddAsync(bookEntity);

            return bookEntity.Id;
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            var bookEntity = await _booksRepository.GetAsync(x => x.Id == id, traked: false);

            return _mapper.Map<Book>(bookEntity);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var booksEntity = await _booksRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Book>>(booksEntity);
        }

        public async Task UpdateBookPatchAsync(Guid bookId, JsonPatchDocument<BookForCreation> model)
        {
            var bookEntity = _mapper.Map<JsonPatchDocument<Entities.Book>>(model);

            await _booksRepository.UpdateBookPatch(bookId, bookEntity);

            await _booksRepository.SaveAsync();
        }
    }
}