using Persistence.Models;
using Persistence.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface IPaymentRequestRepository : IRepository<PaymentRequest>
    {
        PaymentRequest Create(PaymentRequest entity);
        Task<PaymentRequestView> GetPaymentRequest(Guid Id);
        Task<PaymentRequestView> GetPaymentRequest(string reference);
        Task<List<PaymentRequestView>> GetPaymentRequests(string customerId);
        Task<double> GetCustomerPendingBalance(string customerId);
    }
}
