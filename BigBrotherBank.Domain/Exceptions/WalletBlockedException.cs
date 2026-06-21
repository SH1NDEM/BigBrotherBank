using System;
using System.Collections.Generic;
using System.Text;

namespace BigBrotherBank.Domain.Exceptions
{
    public class WalletBlockedException : Exception
    {
        public WalletBlockedException(string message) : base(message)
        { 
        }
    }
}
