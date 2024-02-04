using Microsoft.EntityFrameworkCore;
using Telecom.Domain.Interfaces.Repositories;
using Telecom.Domain.Models;
using Telecom.Infrastructure.Database;

namespace Telecom.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TopUpTransaction> _transactionDbSet;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
            _transactionDbSet = _context.TopUpTransactions;
        }

        public async Task<TopUpTransaction> AddTransaction(TopUpTransaction transaction)
        {
            await _transactionDbSet.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return await GetTransactionById(transaction.Id);
        }

        public Task<bool> DeleteTransaction(int transactionId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TopUpTransaction>> GetAllTransactionsForAccount(int accountId)
        {
            return await _transactionDbSet.Where(i => i.AccountId == accountId).ToListAsync();
        }

        public async Task<TopUpTransaction> GetTransactionById(int transactionId)
        {
            return await _transactionDbSet.FindAsync(transactionId);
        }

        public Task<bool> UpdateTransaction(TopUpTransaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
