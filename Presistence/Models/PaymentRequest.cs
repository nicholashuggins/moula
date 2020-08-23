using System;

namespace Persistence.Models
{
    public class PaymentRequest
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public double Amount { get; set; }
        public int RequestStatusId { get; set; }
        public int StatusReasonId { get; set; }
        public string Reference { get; set; }
    }
}
