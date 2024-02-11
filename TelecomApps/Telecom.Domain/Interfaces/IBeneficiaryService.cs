using Telecom.Domain.Dtos;
using Telecom.Domain.Models;



namespace Telecom.Domain.Interfaces
{
    public interface IBeneficiaryService
    {
        Task<BeneficiaryDto> CreateBeneficiaryAsync(BeneficiaryDto model, int accountId);
  
        Task<IEnumerable<BeneficiaryDto>> GetAllBeneficiaryForAccountAsync(int accountId);

        Task<Beneficiary> GetBeneficiaryByIdAsync(int id);

        Task<Beneficiary> DeleteBeneficiaryAsync(int id);

        Task<bool> UpdateBeneficiary(Beneficiary beneficiary);

        Task<bool> UpdateBeneficiaryRecharge(int beneficiaryId, int rechargeAmount, DateTime date);
        Task<int> ResetTopUpCounterForThisMonth(int id);
    }
}
