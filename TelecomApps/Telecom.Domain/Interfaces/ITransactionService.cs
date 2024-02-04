using Telecom.Domain.Models;

namespace Telecom.Domain.Interfaces
{
    public interface ITransactionService
    {
        Task<TopUpTransaction> AddTransaction(TopUpTransaction transaction);

        Task<TopUpTransaction> GetTransaction(int transactionId);

        Task<IEnumerable<TopUpTransaction>> GetAllTransactionsForAccount(int accountId);
    }
}