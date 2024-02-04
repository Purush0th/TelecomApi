using Telecom.Domain.Interfaces;
using Telecom.Domain.Interfaces.Repositories;
using Telecom.Domain.Models;


namespace Telecom.Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<TopUpTransaction> AddTransaction(TopUpTransaction transaction)
        {
            return await _transactionRepository.AddTransaction(transaction);
        }

        public async Task<IEnumerable<TopUpTransaction>> GetAllTransactionsForAccount(int accountId)
        {
            return await _transactionRepository.GetAllTransactionsForAccount(accountId);
        }

        public async Task<TopUpTransaction> GetTransaction(int transactionId)
        {
            return await _transactionRepository.GetTransactionById(transactionId);
        }
    }
}
