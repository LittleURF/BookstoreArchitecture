using BookstoreArchitecture.Domain.Aggregates.BookAggregate;
using BookstoreArchitecture.Domain.ValueObjects;

namespace BookstoreArchitecture.Domain.Aggregates.CatalogAggregate
{
    public class Book : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public PageCount PageCount { get; private set; }
        public Price Price { get; private set; }

        public Book(string title, int pageCount, decimal price, string currencyCode)
        {
            Id = Guid.NewGuid();
            Title = title;
            PageCount = new(pageCount);
            Price = new(price, currencyCode);
        }

        public void UpdateToPremium()
        {
            Title = $"Premium: {Title}";
            Price = new(Price.Amount * 1.5m, Price.CurrencyCode);

            _domainEvents.Add(new BookUpdatedToPremiumEvent(Id));
        }
    }
}
