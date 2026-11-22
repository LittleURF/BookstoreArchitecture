using BookstoreArchitecture.Application.Services;
using BookstoreArchitecture.Api.Models;
using ApplicationBookDto = BookstoreArchitecture.Application.DTOs.BookDto;
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
        public async Task<BookDto> GetBook(Guid id, CancellationToken cancellationToken)
        {
            var book = await _booksService.GetBookAsync(id, cancellationToken);
            return ToApiDto(book);
        }

        [HttpGet()]
        public async Task<IEnumerable<BookDto>> GetAllBooks(CancellationToken cancellationToken)
        {
            var books = await _booksService.GetAllBooksAsync(cancellationToken);
            return books.Select(ToApiDto);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _booksService.CreateBookAsync(
                request.Title,
                request.PageCount,
                request.Price,
                request.CurrencyCode,
                cancellationToken);

            var response = ToApiDto(book);
            return CreatedAtAction(nameof(GetBook), new { id = response.Id }, response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBook(Guid id, CancellationToken cancellationToken)
        {
            await _booksService.DeleteBookAsync(id, cancellationToken);
            return NoContent();
        }

        private static BookDto ToApiDto(ApplicationBookDto book) =>
            new(book.Id, book.Title, book.PageCount, book.Price);
    }
}
