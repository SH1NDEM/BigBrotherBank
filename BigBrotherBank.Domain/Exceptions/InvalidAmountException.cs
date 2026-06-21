using System;
using System.Collections.Generic;
using System.Text;

namespace BigBrotherBank.Domain.Exceptions
{
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException(string message) : base(message)
        {
        }
    }
}
