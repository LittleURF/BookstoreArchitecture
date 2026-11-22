using BookstoreArchitecture.Application.Repositories;
using BookstoreArchitecture.Application.Exceptions;
using BookstoreArchitecture.Domain.Aggregates.BookAggregate;
using BookstoreArchitecture.Application.Models;

namespace BookstoreArchitecture.Application.Services
{
    public class BooksService(IBooksRepository booksRepository) : IBooksService
    {

        public async Task<BookDto> GetBookAsync(Guid id, CancellationToken cancellationToken)
        {
            var book = await booksRepository.GetBookAsync(id, cancellationToken);
            if (book is null)
            {
                throw new BookNotFoundException(id);
            }

            return ToDto(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            var books = await booksRepository.GetAllBooksAsync(cancellationToken);
            return books.Select(ToDto);
        }

        public async Task<BookDto> CreateBookAsync(string title, int pageCount, decimal price, string currencyCode, CancellationToken cancellationToken)
        {
            var book = new Book(title, pageCount, price, currencyCode);
            await booksRepository.AddBookAsync(book, cancellationToken);
            await booksRepository.SaveChangesAsync(cancellationToken);
            return ToDto(book);
        }

        public async Task DeleteBookAsync(Guid id, CancellationToken cancellationToken)
        {
            await GetBookAsync(id, cancellationToken);
            await booksRepository.DeleteBookAsync(id, cancellationToken);
            await booksRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateBookToPremiumAsync(Guid id, CancellationToken cancellationToken)
        {
            var book = await booksRepository.GetBookAsync(id, cancellationToken);
            if (book is null)
            {
                throw new BookNotFoundException(id);
            }

            book.UpdateToPremium();
            // propgate: book.DomainEvents
            await booksRepository.SaveChangesAsync(cancellationToken);
        }

        private static BookDto ToDto(Book book) =>
            new(book.Id, book.Title, book.PageCount.Value, book.Price.Amount, book.Price.CurrencyCode);
    }
}
