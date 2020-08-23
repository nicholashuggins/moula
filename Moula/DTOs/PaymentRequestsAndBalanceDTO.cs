using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.DTOs
{
    public class PaymentRequestsAndBalanceDTO
    {
        public List<PaymentRequestDTO> PaymentRequests { get; set; }
        public double Balance { get; set; }
    }
}
