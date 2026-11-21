using BookstoreArchitecture.Domain.ValueObjects;

namespace BookstoreArchitecture.Domain.Aggregates.BookAggregate
{
    public class Book : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public PageCount PageCount { get; private set; }
        public Price Price { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private Book()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        {
            
        }

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
