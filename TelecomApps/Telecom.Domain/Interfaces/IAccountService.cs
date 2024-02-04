using Telecom.Domain.Models;

namespace Telecom.Domain.Interfaces
{
    public interface IAccountService
    {
        Task<int> UpdateCreditForThisMonth(int accountId);

        Task<bool> DebitAmount(int accountId, int Amount);

        Task<Tuple<int, int, DateTime?>> CheckCredit(string accountId);

        Task<Account> GetAccountById(int accountId);

        Task<Account> GetAccountWithBeneficiariesById(int accountId);

    }
}
