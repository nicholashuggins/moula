using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using Persistence.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(MoulaContext context) : base(context)
        {
        }
        public async Task<double> GetCustomerBalance(string customerId)
        {
            var transactions = await GetCustomerTransactions(customerId);
            var amount = transactions.Sum(t => t.Amount);
            return amount;
        }

        private Task<List<Transaction>> GetCustomerTransactions(string customerId)
        {
            var result = Context.Set<Transaction>().AsAsyncEnumerable().Where(t => t.CustomerId == customerId).ToListAsync();
            var transactions = result.AsTask();
            return transactions;
        }


    }
}
