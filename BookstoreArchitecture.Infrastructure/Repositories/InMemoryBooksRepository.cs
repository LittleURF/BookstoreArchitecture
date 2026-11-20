using BookstoreArchitecture.Application.Repositories;
using BookstoreArchitecture.Domain.Aggregates.BookAggregate;

namespace BookstoreArchitecture.Infrastructure.Repositories;

public sealed class InMemoryBooksRepository : IBooksRepository
{
    private readonly List<Book> _books =
    [
        new Book("Domain-Driven Design", 560, 59.99m, "USD"),
        new Book("Clean Architecture", 432, 44.99m, "USD"),
        new Book("The Pragmatic Programmer", 352, 39.99m, "USD")
    ];

    public Task<Book?> GetBookAsync(Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_books.FirstOrDefault(book => book.Id == id));
    }

    public Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IEnumerable<Book>>(_books.ToArray());
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.CompletedTask;
    }
}
