using BookstoreArchitecture.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreArchitecture.Domain.Aggregates.BookAggregate
{
    public record BookUpdatedToPremiumEvent(Guid BookId) : DomainEvent;
}
