using System;
using System.Collections.Generic;
using System.Text;

namespace BigBrotherBank.Domain.Enums
{
    public enum TransactionState
    {
        Pending,
        Completed,
        Failed,
        Cancelled
    }
}
