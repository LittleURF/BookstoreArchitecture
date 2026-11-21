
using BookstoreArchitecture.Application.Exceptions;
using BookstoreArchitecture.Application.Repositories;
using BookstoreArchitecture.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BookstoreArchitecture.Infrastructure.Repositories;

public class BooksRepository : IBooksRepository
{
    private readonly BookstoreDbContext _dbContext;

    public BooksRepository(BookstoreDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task AddBookAsync(Book book, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(book, cancellationToken);
    }

    public async Task DeleteBookAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
        if (book is null)
        {
            throw new BookNotFoundException(id);
        }

        _dbContext.Remove(book);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Books.ToListAsync(cancellationToken);
    }

    public async Task<Book?> GetBookAsync(Guid id, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
        return book;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync();
    }
}
