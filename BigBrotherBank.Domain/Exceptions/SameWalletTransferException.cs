using System;
using System.Collections.Generic;
using System.Text;

namespace BigBrotherBank.Domain.Exceptions
{
    internal class SameWalletTransferException : Exception
    {
        public SameWalletTransferException(string message) : base(message)
        {
        }
    }
}
