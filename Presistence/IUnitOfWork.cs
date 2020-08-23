using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IPaymentRequestRepository PaymentRequests { get; }
        ITransactionRepository Transactions { get; }
        Task<int> Complete();
    }
}
