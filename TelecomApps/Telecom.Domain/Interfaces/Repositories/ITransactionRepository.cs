

using Telecom.Domain.Models;

namespace Telecom.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        public Task<TopUpTransaction> AddTransaction(TopUpTransaction transaction);

        public Task<TopUpTransaction> GetTransactionById(int transactionId);

        public Task<IEnumerable<TopUpTransaction>> GetAllTransactionsForAccount(int accountId);

    }
}
