using Microsoft.Extensions.Options;
using Telecom.Domain.Configuration;
using Telecom.Domain.Dtos;
using Telecom.Domain.Interfaces;
using Telecom.Domain.Models;

namespace Telecom.Application.Services
{
    public class RechargeService : IRechargeService
    {
        private IPaymentService _paymentService;
        private IBeneficiaryService _beneficiaryService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly AppSettings _appSettings;

        public RechargeService(IPaymentService paymentService, IBeneficiaryService beneficiaryService, IAccountService accountService, ITransactionService transactionService, IOptions<AppSettings> appSettings)
        {
            _paymentService = paymentService;
            _beneficiaryService = beneficiaryService;
            _accountService = accountService;
            _transactionService = transactionService;
            _appSettings = appSettings.Value;

        }

        public async Task<Tuple<RechargeRequestDto, bool>> DoRecharge(RechargeRequestDto rechargeRequest)
        {

            try
            {
                var beneficiaryDetails = await _beneficiaryService.GetBeneficiaryByIdAsync(rechargeRequest.BeneficiaryId);
                if (beneficiaryDetails == null)
                    throw new Exception($"Beneficiary Not Found. Id : {rechargeRequest.BeneficiaryId}");

                int maxTopupPerUserThisMonth = GetTopupThersholdOnUserVerification(beneficiaryDetails);

                var currentAccount = beneficiaryDetails.Account;
                var accountAvailableCredit = currentAccount.AvailableTopUpCredit;
                var beneficiaryThisMonthTopUpTotal = beneficiaryDetails.TopUpTotal;

                if (!IsDateInCurrentMonth(currentAccount.LastRechargeDate))
                {
                    accountAvailableCredit = await _accountService.UpdateCreditForThisMonth(currentAccount.Id);
                    beneficiaryThisMonthTopUpTotal = await _beneficiaryService.ResetTopUpCounterForThisMonth(beneficiaryDetails.Id);
                }

                var beneficiaryAfterTopupMonthly = beneficiaryThisMonthTopUpTotal + rechargeRequest.Amount;

                if (beneficiaryAfterTopupMonthly > maxTopupPerUserThisMonth)
                    throw new Exception($"Maximum allowed recharge limit exceed for this month. Beneficiart: {beneficiaryDetails.NickName}, Total Topup: {beneficiaryDetails.TopUpTotal},  Requested TopUp: {rechargeRequest.Amount}.");

                if (rechargeRequest.Amount > accountAvailableCredit)
                    throw new Exception($"Not enought credit. Requested TopUp: {rechargeRequest.Amount}, Available Credit: {accountAvailableCredit}");

                var paymentResponse = await _paymentService.Charge(rechargeRequest.Amount);
                await AddTransactionAsync(currentAccount.Id, rechargeRequest.BeneficiaryId, rechargeRequest.Amount, paymentResponse.Item1, paymentResponse.Item2, paymentResponse.Item3);

                if (!paymentResponse.Item1)
                    throw new Exception($"Payment Error: {paymentResponse.Item3}");

                await _accountService.DebitAmount(currentAccount.Id, rechargeRequest.Amount);
                await _beneficiaryService.UpdateBeneficiaryRecharge(beneficiaryDetails.Id, rechargeRequest.Amount, DateTime.Now);

                return new Tuple<RechargeRequestDto, bool>(rechargeRequest, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int GetTopupThersholdOnUserVerification(Beneficiary beneficiaryDetails)
        {
            return beneficiaryDetails.Account.User.IsVerified ?
                     _appSettings.MaxTopUpPerMonthPerVerifiedUser :
                     _appSettings.MaxTopUpPerMonthPerUnVerifiedUser;
        }

        private async Task AddTransactionAsync(int accountId, int beneficiaryId, int amount, bool isSuccess, string bankTransactionId, string errorMessage)
        {
            var transation = new TopUpTransaction();
            transation.AccountId = accountId;
            transation.Amount = amount;
            transation.BeneficiaryId = beneficiaryId;
            transation.BankTransactionId = bankTransactionId;
            transation.IsSuccess = isSuccess;
            transation.Date = DateTime.UtcNow;
            transation.ErrorDetails = errorMessage;


            await _transactionService.AddTransaction(transation);

        }

        private bool IsDateInCurrentMonth(DateTime? date)
        {
            if (date == null)
                return true;

            DateTime currentDate = DateTime.UtcNow;
            return date?.Year == currentDate.Year && date?.Month == currentDate.Month;
        }
    }
}
