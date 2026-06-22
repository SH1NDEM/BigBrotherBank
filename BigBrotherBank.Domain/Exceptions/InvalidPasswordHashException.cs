using System;
using System.Collections.Generic;
using System.Text;

namespace BigBrotherBank.Domain.Exceptions
{
    internal class InvalidPasswordHashException : Exception
    {
        public InvalidPasswordHashException(string message) : base(message)
        {
        }
    }
}
