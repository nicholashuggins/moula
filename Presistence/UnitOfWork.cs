using Persistence.Repositories;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoulaContext _context;
        public ITransactionRepository Transactions { get; }
        public IPaymentRequestRepository PaymentRequests { get; }
        public UnitOfWork(MoulaContext context)
        {
            _context = context;
            PaymentRequests = new PaymentRequestRepository(_context);
            Transactions = new TransactionRepository(_context);
        }
        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
