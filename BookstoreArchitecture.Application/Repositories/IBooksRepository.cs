using BookstoreArchitecture.Domain.Aggregates.BookAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreArchitecture.Application.Repositories
{
    public interface IBooksRepository
    {
        Task<Book?> GetBookAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken);
        Task AddBookAsync(Book book, CancellationToken cancellationToken);
        Task DeleteBookAsync(Guid id, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
