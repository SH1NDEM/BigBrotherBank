using System;
using System.Collections.Generic;
using System.Text;

namespace BigBrotherBank.Domain.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException(string message) : base(message)
        {
        }
    }
}
