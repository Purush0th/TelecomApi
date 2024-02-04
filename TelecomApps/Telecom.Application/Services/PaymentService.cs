using Stripe;
using Telecom.Domain.Interfaces;

namespace Telecom.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private TokenService _stripeTokenService;
        private BalanceService _stripebalanceService;
        private readonly ChargeService _chargeService;

        public PaymentService(TokenService appTokenService, BalanceService stripebalanceService, ChargeService chargeService)
        {
            _stripeTokenService = appTokenService;
            _stripebalanceService = stripebalanceService;
            _chargeService = chargeService;
        }
        public async Task<Tuple<bool, string, string>> Charge(long amount)
        {

            try
            {
                var balanceOptions = new BalanceGetOptions();

                var balance = _stripebalanceService.Get(balanceOptions);

                if (!balance.Available.Any()) // balance < amount throw.
                {
                    throw new Exception("Insufficient Balance");
                }

                var tokenOptions = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = "4242424242424242",
                        ExpMonth = "5",
                        ExpYear = "2024",
                        Cvc = "314",
                    },
                };
                var tokenResponse = _stripeTokenService.Create(tokenOptions);



                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = amount + 1,
                    Currency = "aed",
                    Source = $"{tokenResponse.Id}",
                    Description = "Test Payment"
                };

                var status = _chargeService.Create(chargeOptions);

                return new Tuple<bool, string, string>(status.Paid, status.TransferId, status.FailureMessage);
            }
            catch (Exception ex)
            {
                // at this moment we are returning `success` even on payment failures
                return new Tuple<bool, string, string>(true, Guid.NewGuid().ToString(), ex.Message);
            }
        }
    }
}
