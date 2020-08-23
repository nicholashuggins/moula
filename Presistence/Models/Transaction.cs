using System;

namespace Persistence.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public double Amount { get; set; }
    }
}
