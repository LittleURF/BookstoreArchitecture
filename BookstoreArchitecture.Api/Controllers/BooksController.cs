using BookstoreArchitecture.Application.Services;
using BookstoreArchitecture.Domain.Aggregates.BookAggregate;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreArchitecture.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;

        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet("{id:guid}")]
        public async Task<Book> GetBook(Guid id, CancellationToken cancellationToken)
        {
            return await _booksService.GetBookAsync(id, cancellationToken);
        }

        [HttpGet()]
        public async Task<IEnumerable<Book>> GetAllBooks(CancellationToken cancellationToken)
        {
            return await _booksService.GetAllBooksAsync(cancellationToken);
        }
    }
}
