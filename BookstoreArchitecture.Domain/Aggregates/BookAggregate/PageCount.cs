using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreArchitecture.Domain.Aggregates.BookAggregate
{
    public record PageCount
    {
        public int Value { get; }
        public PageCount(int count)
        {
            if (count < 0)
            {
                throw new InvalidPagesCountException("Pages count cannot be negative.");
            }

            if (count == 0)
            {
                throw new InvalidPagesCountException("Pages count cannot be zero.");
            }

            Value = count;
        }
    }
}
