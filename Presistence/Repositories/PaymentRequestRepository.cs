using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using Persistence.QueryModels;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Persistence.Enums.Enumerations;

namespace Persistence.Repositories
{
    public class PaymentRequestRepository : Repository<PaymentRequest>, IPaymentRequestRepository
    {
        public PaymentRequestRepository(MoulaContext context) : base(context)
        {

        }
        public PaymentRequest Create(PaymentRequest entity)
        {
            entity.Id = GetNewKey();
            entity.CreatedOn = DateTime.Now;
            entity.Reference = $"{entity.CustomerId}{entity.CreatedOn.ToString("dd-MM-yyyy HH:mm:ss.fffffff")}";
            return Add(entity);
        }
        public async Task<PaymentRequestView> GetPaymentRequest(Guid id)
        {
            var sql = $"exec sp_executesql N'exec GetPaymentRequest ''{id}'''";
            var result = Context.Set<PaymentRequestView>().FromSqlRaw<PaymentRequestView>(sql).AsAsyncEnumerable<PaymentRequestView>().FirstOrDefaultAsync();
            return await result.AsTask();
        }
        public async Task<PaymentRequestView> GetPaymentRequest(string reference)
        {
            var sql = $"exec sp_executesql N'exec GetPaymentRequestByReference ''{reference}'''";
            var result = Context.Set<PaymentRequestView>().FromSqlRaw<PaymentRequestView>(sql).AsAsyncEnumerable<PaymentRequestView>().FirstOrDefaultAsync();
            return await result.AsTask();

        }
        public async Task<List<PaymentRequestView>> GetPaymentRequests(string customerId)
        {
            var sql = $"exec sp_executesql N'exec GetPaymentRequests ''{customerId}'''";
            var result = Context.Set<PaymentRequestView>().FromSqlRaw<PaymentRequestView>(sql).AsAsyncEnumerable<PaymentRequestView>().ToListAsync();
            return await result.AsTask();
        }

        public async Task<double> GetCustomerPendingBalance(string customerId)
        {
            var payments = await GetCustomerPendingPayments(customerId);
            var amount = payments.Sum(t => t.Amount);
            return amount;
        }

        private Task<List<PaymentRequest>> GetCustomerPendingPayments(string customerId)
        {
            var result = Context.Set<PaymentRequest>().AsAsyncEnumerable().Where(t => t.CustomerId == customerId && t.RequestStatusId == (int)StatusOfRequest.Pending).ToListAsync();
            var payments = result.AsTask();
            return payments;
        }


    }
}
