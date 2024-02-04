using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telecom.Domain.Models;

namespace Telecom.Domain.Interfaces.Repositories
{
    public interface IBeneficiaryRepository
    {
        Task<Beneficiary> CreateBeneficiary(Beneficiary beneficiary, int accountId);
        Task<int> GetBeneficiariesCoundForAccount(int accountId);
        Task<IEnumerable<Beneficiary>> GetBeneficiariesOfAccountAsync(int accountId);
        Task<Beneficiary> GetBeneficiaryById(int id);
        Task<bool> UpdateBeneficiary(Beneficiary beneficiary);
        Task<bool> UpdateBeneficiaryRecharge(int beneficiaryId, int rechargeAmount, DateTime date);
    }
}
