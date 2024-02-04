using Microsoft.AspNetCore.Mvc;
using Telecom.Domain.Interfaces;
using Telecom.Domain.Models;
using Telecom.Domain.ViewModels;

namespace Telecom.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionLogController : Controller
    {
        private ITransactionService _topupTransactionService;

        public TransactionLogController(ITransactionService service)
        {
            _topupTransactionService = service;
        }

        [Route("account/{{accoundId}}/GetAll")]
        [HttpGet]
        public async Task<IEnumerable<TopUpTransaction>> GetAll(int accountId)
        {
            var beneficiaries = await _topupTransactionService.GetAllTransactionsForAccount(accountId);

            return beneficiaries;
        }
    }
}
