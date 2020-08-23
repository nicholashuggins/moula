using Moula.DTOs;
using Persistence.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moula.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentRequestDTO> CreatePaymentRequest(PaymentRequestDTO request);
        Task<PaymentRequestDTO> CancelPaymentRequest(string reference);
        Task<PaymentRequestDTO> ProcessPaymentRequest(string reference);
        Task<ICollection<PaymentRequestDTO>> GetPaymentRequests(string customerId);
        Task<double> GetBalance (string customerId);
        Task<PaymentRequestsAndBalanceDTO> GetPaymentRequestsAndBalance(string customerId);

    }
}
