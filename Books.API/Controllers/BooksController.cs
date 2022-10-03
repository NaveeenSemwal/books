using Azure.Identity;
using Azure.Storage.Blobs;
using Books.API.Models;
using Books.API.Services;
using Books.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Books.API.Controllers
{
    /// <summary
    /// https://code-maze.com/global-error-handling-aspnetcore/
    /// 
    /// https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-add-app-roles-in-azure-ad-apps
    /// 
    /// https://www.youtube.com/watch?v=xEvSFyXBX58&list=PLUGuCqrhcwZzht4r2sbByidApmrvEjL9m
    /// </summary>
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksServive _booksServive;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IBooksServive booksServive, ILogger<BooksController> logger)
        {
            _booksServive = booksServive ??
                throw new ArgumentNullException(nameof(booksServive));
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Book.ReadAll")]
        public async Task<IActionResult> GetBooks()
        {
            var bookEntities = await _booksServive.GetBooksAsync();
            return Ok(bookEntities);
        }



        [HttpGet]
        [Route("{id}", Name = "GetBook")]
        [Authorize(Roles = "Agent")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Book.Read")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var bookEntity = await _booksServive.GetBookAsync(id);
            if (bookEntity == null)
            {
                return NotFound();
            }
            //var bookCovers = await _booksRepository.GetBookCoversAsync(id);

            //var propertyBag = new Tuple<Entities.Book, IEnumerable<ExternalModels.BookCover>>
            //    (bookEntity, bookCovers);

            //(Entities.Book book, IEnumerable<ExternalModels.BookCover> bookCovers) 
            //    propertyBag = (bookEntity, bookCovers);

            return Ok((bookEntity));
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes:Book.Create")]
        public async Task<IActionResult> CreateBook([FromForm] BookForCreation bookForCreation)  /*[FromForm] - fix-415-unsupported-media-type-on-file-upload*/
        {
            BookMessageProducer messageProducer = new BookMessageProducer();

            // Store first in local drive and then in BLOB
            string path = Path.Combine(Directory.GetCurrentDirectory(), "images", bookForCreation.FormFile.FileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                bookForCreation.FormFile.CopyTo(stream);

                // Invoking an event
                messageProducer.AddBookToQueue(new BookEventArgs { AuthorId = bookForCreation.AuthorId, Description = bookForCreation.Description, Title = bookForCreation.Title, File = stream });
                stream.Flush();
            }

            var bookId = await _booksServive.AddBook(bookForCreation);

            Book bookEntity = await _booksServive.GetBookAsync(bookId);

            // To return 201 status and "GetBook" is name of route defined above.
            return CreatedAtRoute("GetBook", new { id = bookId }, bookEntity);
        }

        [HttpPost(nameof(UploadFile))]
        public IActionResult UploadFile(IFormFile files)
        {
            string systemFileName = files.FileName;

            return Ok(files);
        }


    }
}
