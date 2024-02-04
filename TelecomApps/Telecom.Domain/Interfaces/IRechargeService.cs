using Telecom.Domain.Dtos;

namespace Telecom.Domain.Interfaces
{
    public interface IRechargeService
    {
        public Task<Tuple<RechargeRequestDto, bool>> DoRecharge(RechargeRequestDto rechargeRequest);
    }
}
