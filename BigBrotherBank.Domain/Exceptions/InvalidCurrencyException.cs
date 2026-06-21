using System;
using System.Collections.Generic;
using System.Text;

namespace BigBrotherBank.Domain.Exceptions
{
    internal class InvalidCurrencyException : Exception
    {
        public InvalidCurrencyException(string message) : base(message)
        {
        }
    }
}
