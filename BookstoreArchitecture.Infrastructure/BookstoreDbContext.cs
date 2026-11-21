using BookstoreArchitecture.Domain.Aggregates.BookAggregate;
using Microsoft.EntityFrameworkCore;

namespace BookstoreArchitecture.Infrastructure
{
    public class BookstoreDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(book =>
            {
                book.Property(book => book.PageCount)
                    .HasColumnType("int")
                    .HasConversion(pageCount => pageCount.Value, value => new PageCount(value));

                book.ComplexProperty(book => book.Price, price =>
                {
                    price.Property(p => p.Amount).HasColumnName("Price");
                    price.Property(p => p.CurrencyCode).HasColumnName("CurrencyCode");
                });

                book.Ignore("DomainEvents");
            });





            base.OnModelCreating(modelBuilder);
        }
    }
}
