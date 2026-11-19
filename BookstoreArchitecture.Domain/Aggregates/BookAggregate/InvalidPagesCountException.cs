using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreArchitecture.Domain.Aggregates.BookAggregate
{
    public class InvalidPagesCountException : Exception
    {
        public InvalidPagesCountException()
        {
            
        }

        public InvalidPagesCountException(string message) : base(message)
        {
        }

        public InvalidPagesCountException(string message, Exception innerException)
        {
        }
    }
}
