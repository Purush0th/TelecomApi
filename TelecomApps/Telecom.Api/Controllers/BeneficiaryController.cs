using Microsoft.AspNetCore.Mvc;
using Telecom.Domain.Interfaces;
using Telecom.Domain.Models;
using Telecom.Domain.ViewModels;

namespace Telecom.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BeneficiaryController : Controller
    {
        private readonly IBeneficiaryService _beneficiaryService;

        public BeneficiaryController(IBeneficiaryService beneficiaryService)
        {
            _beneficiaryService = beneficiaryService;
        }

        [Route("account/{{accoundId}}/GetAll")]
        [HttpGet]
        public async Task<IEnumerable<BeneficiaryDto>> Get(int accountId)
        {
            var beneficiaries = await _beneficiaryService.GetAllBeneficiaryForAccountAsync(accountId);

            return beneficiaries;
        }


        [Route("account/{{accoundId}}/Create")]
        [HttpPost]
        public async Task<BeneficiaryDto> Create(int accountId, [FromBody] BeneficiaryDto beneficiary)
        {
            var item = await _beneficiaryService.CreateBeneficiaryAsync(beneficiary, accountId);

            return item;
        }
    }
}
