using Books.API.Models;
using Books.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
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

        public BooksController(IBooksServive booksServive,ILogger<BooksController> logger)
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
        public async Task<IActionResult> CreateBook(BookForCreation bookForCreation)
        {
            var bookId = await _booksServive.AddBook(bookForCreation);

            Book bookEntity = await _booksServive.GetBookAsync(bookId);

            // To return 201 status and "GetBook" is name of route defined above.
            return CreatedAtRoute("GetBook", new { id = bookId }, bookEntity);
        }
    }
}
