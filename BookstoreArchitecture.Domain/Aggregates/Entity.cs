using BookstoreArchitecture.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreArchitecture.Domain.Aggregates
{
    public class Entity
    {
        public IEnumerable<DomainEvent> DomainEvents => _domainEvents.AsEnumerable();
        protected readonly List<DomainEvent> _domainEvents = [];
    }
}
