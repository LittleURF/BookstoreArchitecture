using BookstoreArchitecture.Application.DTOs;

namespace BookstoreArchitecture.Application.Services
{
    public interface IBooksService
    {
        Task<BookDto> GetBookAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<BookDto>> GetAllBooksAsync(CancellationToken cancellationToken);
        Task<BookDto> CreateBookAsync(string title, int pageCount, decimal price, string currencyCode, CancellationToken cancellationToken);
        Task DeleteBookAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateBookToPremiumAsync(Guid id, CancellationToken cancellationToken);
    }
}