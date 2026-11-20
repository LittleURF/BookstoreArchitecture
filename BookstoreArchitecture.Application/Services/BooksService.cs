using BookstoreArchitecture.Application.Repositories;
using BookstoreArchitecture.Application.Exceptions;
using BookstoreArchitecture.Domain.Aggregates.BookAggregate;

namespace BookstoreArchitecture.Application.Services
{
    public class BooksService(IBooksRepository booksRepository) : IBooksService
    {

        public async Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken)
        {
            var book = await booksRepository.GetBookAsync(id, cancellationToken);
            if (book is null)
            {
                throw new BookNotFoundException(id);
            }

            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            return await booksRepository.GetAllBooksAsync(cancellationToken);
        }

        public async Task<Book> CreateBookAsync(string title, int pageCount, decimal price, string currencyCode, CancellationToken cancellationToken)
        {
            var book = new Book(title, pageCount, price, currencyCode);
            await booksRepository.AddBookAsync(book, cancellationToken);
            await booksRepository.SaveChangesAsync(cancellationToken);
            return book;
        }

        public async Task DeleteBookAsync(Guid id, CancellationToken cancellationToken)
        {
            await GetBookAsync(id, cancellationToken);
            await booksRepository.DeleteBookAsync(id, cancellationToken);
            await booksRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateBookToPremiumAsync(Guid id, CancellationToken cancellationToken)
        {
            var book = await GetBookAsync(id, cancellationToken);
            book.UpdateToPremium();
            // propgate: book.DomainEvents
            await booksRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
