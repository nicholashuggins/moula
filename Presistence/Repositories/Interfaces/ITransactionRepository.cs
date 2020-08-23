using Persistence.Models;
using System.Threading.Tasks;

namespace Persistence.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<double> GetCustomerBalance(string customerId);
    }
}
