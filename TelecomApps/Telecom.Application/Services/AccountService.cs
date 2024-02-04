
using Telecom.Domain.Interfaces;
using Telecom.Domain.Models;
using Telecom.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Options;
using Telecom.Domain.Configuration;

namespace Telecom.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly AppSettings _appSettings;

        public AccountService(IAccountRepository accountRepo, IOptions<AppSettings> appSettings)
        {
            _accountRepo = accountRepo;
            _appSettings = appSettings.Value;
        }
        public Task<Tuple<int, int, DateTime?>> CheckCredit(string accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DebitAmount(int accountId, int Amount)
        {
            var account = await _accountRepo.GetAccount(accountId);
            account.LastRechargeDate = DateTime.UtcNow;
            account.AvailableTopUpCredit -= Amount;

            return await _accountRepo.UpdateAccount(accountId, account);

        }

        public async Task<Account> GetAccountById(int accountId)
        {
            return await _accountRepo.GetAccount(accountId);
        }

        public async Task<Account> GetAccountWithBeneficiariesById(int accountId)
        {
            return await _accountRepo.GetAccountWithBeneficiaries(accountId);
        }

        public async Task<int> UpdateCreditForThisMonth(int accountId)
        {
            var newCredit = _appSettings.MaxTopUpPerMonthForAccount;
            var account = new Account
            {
                AvailableTopUpCredit = newCredit,
                TotalTopUpCredit = newCredit,
                LastRechargeDate = null
            };
            var result = await _accountRepo.UpdateAccount(accountId, account);

            return result ? newCredit : 0;
        }
    }
}
