using Microsoft.Extensions.Options;
using Moq;
using Telecom.Application.Services;
using Telecom.Domain.Configuration;
using Telecom.Domain.Interfaces;
using Telecom.Domain.Models;

namespace TelecomApps.Tests.Fixtures
{
    public class RechargeServiceFixture : IDisposable
    {
        private readonly Mock<IPaymentService> _paymentService;
        private readonly Mock<IBeneficiaryService> _beneficiaryService;
        private readonly Mock<IAccountService> _accountService;
        private readonly Mock<ITransactionService> _transactionService;
        private readonly Mock<IOptions<AppSettings>> _appSettings;

        public RechargeService RechargeService;
        public RechargeServiceFixture()
        {
            _paymentService = new Mock<IPaymentService>();
            _beneficiaryService = new Mock<IBeneficiaryService>();
            _transactionService = new Mock<ITransactionService>();
            _accountService = new Mock<IAccountService>();
            _appSettings = new Mock<IOptions<AppSettings>>();

            _appSettings.Setup(i => i.Value).Returns(new AppSettings() { MaxTopUpPerMonthPerVerifiedUser = 1000, MaxTopUpPerMonthPerUnVerifiedUser = 500 });

            RechargeService = new RechargeService(_paymentService.Object, _beneficiaryService.Object, _accountService.Object, _transactionService.Object, _appSettings.Object);
        }
        public void Dispose()
        {

        }

        public void SetupBeneficiaryDetails(int beneficiaryId, int beneficiaryTopUpTotal, bool isVerifiedUser, int AccountTopUpCredit, int AccountAvailableCredit)
        {
            _beneficiaryService.Setup(i => i.GetBeneficiaryByIdAsync(beneficiaryId)).ReturnsAsync(new Beneficiary
            {
                Id = beneficiaryId,
                NickName = "Test Beneficiary",
                PhoneNumber = "+971-12-1234567",
                TopUpTotal = beneficiaryTopUpTotal,
                Account = new Account
                {
                    TotalTopUpCredit = AccountTopUpCredit,
                    AvailableTopUpCredit = AccountAvailableCredit,
                    Id = 1,
                    LastRechargeDate = DateTime.UtcNow.AddDays(-7),
                    User = new User { Id = 1, IsVerified = isVerifiedUser }
                }
            });
        }

        public void SetupPayment(long topUpAmount)
        {
            _paymentService.Setup(i => i.Charge(topUpAmount)).ReturnsAsync(new Tuple<bool, string, string, long>(true, new Guid().ToString(), "", topUpAmount + 1));
        }


    }
}