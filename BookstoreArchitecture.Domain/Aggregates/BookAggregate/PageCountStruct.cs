using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreArchitecture.Domain.Aggregates.BookAggregate
{
    internal struct PageCountStruct
    {
        public PageCountStruct(int count)
        {
            if (count < 0)
            {
                throw new InvalidPagesCountException("Pages count cannot be negative.");
            }

            if (count == 0)
            {
                throw new InvalidPagesCountException("Pages count cannot be zero.");
            }
        }

        public int Sum()
        {
            throw new NotImplementedException();
        }
    }
}
