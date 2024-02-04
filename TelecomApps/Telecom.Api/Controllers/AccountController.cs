using Microsoft.AspNetCore.Mvc;
using Telecom.Domain.Interfaces;
using Telecom.Domain.Models;

namespace Telecom.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("{{accountId}}")]
        public async Task<Account> GetAccount(int accountId)
        {
            return await _accountService.GetAccountWithBeneficiariesById(accountId);
        }
    }
}
