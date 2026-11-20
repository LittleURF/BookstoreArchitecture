using BookstoreArchitecture.Domain.Aggregates.BookAggregate;

namespace BookstoreArchitecture.Application.Services
{
    public interface IBooksService
    {
        Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken);
        Task<Book> CreateBookAsync(string title, int pageCount, decimal price, string currencyCode, CancellationToken cancellationToken);
        Task DeleteBookAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateBookToPremiumAsync(Guid id, CancellationToken cancellationToken);
    }
}