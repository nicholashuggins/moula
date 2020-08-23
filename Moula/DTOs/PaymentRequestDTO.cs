using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.DTOs
{
    public class PaymentRequestDTO
    {
        public string CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public double Amount { get; set; }
        public string Reference { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
    }
}
