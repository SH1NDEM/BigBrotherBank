using System;
using System.Collections.Generic;
using System.Text;

namespace BigBrotherBank.Domain.Entities
{
    internal class User
    {
        private readonly Guid Id;
        private string Email = "";
        private string PasswardHash = "";
        private readonly DateTime CreatedAtUtc;
    }
}
