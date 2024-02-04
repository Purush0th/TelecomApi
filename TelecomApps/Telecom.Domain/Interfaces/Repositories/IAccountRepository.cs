using Telecom.Domain.Models;

namespace Telecom.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        public Task<Account> GetAccount(int id);

        public Task<Account> GetAccountWithBeneficiaries(int id);

        public Task<bool> UpdateAccount(int id, Account account);

        public Task<bool> DeleteAccount(int id);

    }
}
