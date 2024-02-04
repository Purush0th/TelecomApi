using Telecom.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Telecom.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Telecom.Domain.Configuration;

namespace Telecom.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RechargeController : Controller
    {
        private readonly IRechargeService _rechargeService;
        private readonly AppSettings _appSettings;

        public RechargeController(IRechargeService rechargeService, IOptions<AppSettings> appSettings)
        {
            _rechargeService = rechargeService;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("topup")]
        public async Task<IActionResult> TopUp([FromBody] RechargeRequestDto recharge)
        {
            try
            {

                var plans = _appSettings.AllowedRechargePlans;
                if (!plans.Any(i => i == recharge.Amount))
                {
                    return BadRequest($"Enter a valid recharge amount. Available Plans: {string.Join(",", plans)}");
                }

                var status = await _rechargeService.DoRecharge(recharge);

                if (status.Item2)
                {
                    return Ok(recharge);
                }

                throw new Exception("Something went wrong!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error", ErrorCode = 500, Details = ex.Message });
            }


        }

    }
}
