using BookstoreArchitecture.Application.Services;
using BookstoreArchitecture.Api.Models;
using BookstoreArchitecture.Domain.Aggregates.BookAggregate;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreArchitecture.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
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

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(CreateBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _booksService.CreateBookAsync(
                request.Title,
                request.PageCount,
                request.Price,
                request.CurrencyCode,
                cancellationToken);

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken)
        {
            await _booksService.DeleteBookAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
