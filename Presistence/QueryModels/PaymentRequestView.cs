using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Persistence.QueryModels
{
    [NotMapped]
    public class PaymentRequestView
    {
        public Guid Id { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public double Amount { get; set; }
        public string RequestStatus { get; set; }
        public string StatusReason { get; set; }
        public string Reference { get; set; }
        public int RequestStatusId { get; set; }
        public int StatusReasonId { get; set; }

    }
}
