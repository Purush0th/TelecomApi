using Microsoft.EntityFrameworkCore;
using Telecom.Domain.Interfaces.Repositories;
using Telecom.Domain.Models;
using Telecom.Infrastructure.Database;

namespace Telecom.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Account> _accountEntities;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
            _accountEntities = context.Accounts;
        }
        public Task<bool> DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetAccount(int id)
        {
            return await _accountEntities.FindAsync(id);
        }

        public async Task<Account> GetAccountWithBeneficiaries(int id)
        {
            return await _accountEntities.Include(i => i.Beneficiaries).Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAccount(int id, Account account)
        {
            try
            {
                var accountEntity = await _accountEntities.FindAsync(id);
                if (accountEntity == null)
                {
                    return false;
                }

                accountEntity.TotalTopUpCredit = account.TotalTopUpCredit;
                accountEntity.AvailableTopUpCredit = account.AvailableTopUpCredit;
                accountEntity.LastRechargeDate = account.LastRechargeDate;

                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
