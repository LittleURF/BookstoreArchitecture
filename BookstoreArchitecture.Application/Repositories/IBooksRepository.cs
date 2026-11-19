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
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
