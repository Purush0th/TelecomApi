
using Telecom.Domain.Configuration;
using Telecom.Domain.Interfaces;
using Telecom.Domain.Interfaces.Repositories;
using Telecom.Domain.Models;
using Microsoft.Extensions.Options;
using AutoMapper;
using Telecom.Domain.Dtos;

namespace Telecom.Application.Services
{
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepo;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;

        public BeneficiaryService(IBeneficiaryRepository repository, IOptions<AppSettings> appSettings, IMapper mapper)
        {
            _beneficiaryRepo = repository;
            _appSettings = appSettings.Value;
            _mapper = mapper;

        }
        public async Task<BeneficiaryDto> CreateBeneficiaryAsync(BeneficiaryDto model, int accountId)
        {
            var count = await _beneficiaryRepo.GetBeneficiariesCoundForAccount(accountId);

            if (count < _appSettings.MaxBeneficiaryPerAccount)
            {
                var beneficiary = new Beneficiary { NickName = model.NickName, PhoneNumber = model.PhoneNumber };
                var createdBeneficiary = await _beneficiaryRepo.CreateBeneficiary(beneficiary, accountId);

                return _mapper.Map<BeneficiaryDto>(createdBeneficiary);
            }


            throw new Exception("Max beneficiary reached");
        }

        public Task<Beneficiary> DeleteBeneficiaryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BeneficiaryDto>> GetAllBeneficiaryForAccountAsync(int accountId)
        {
            var list = await _beneficiaryRepo.GetBeneficiariesOfAccountAsync(accountId);
            return _mapper.Map<IEnumerable<BeneficiaryDto>>(list);
        }

        public async Task<Beneficiary> GetBeneficiaryByIdAsync(int id)
        {
            return await _beneficiaryRepo.GetBeneficiaryById(id);
        }

        public async Task<bool> UpdateBeneficiary(Beneficiary beneficiary)
        {
            return await _beneficiaryRepo.UpdateBeneficiary(beneficiary);
        }

        public async Task<bool> UpdateBeneficiaryRecharge(int beneficiaryId, int rechargeAmount, DateTime date)
        {
            return await _beneficiaryRepo.UpdateBeneficiaryRecharge(beneficiaryId, rechargeAmount, date);
        }

        public async Task<int> ResetTopUpCounterForThisMonth(int id)
        {
            var beneficiary = await GetBeneficiaryByIdAsync(id);
            beneficiary.TopUpTotal = 0;
            beneficiary.LastRechargeAmount = 0;
            beneficiary.LastRechargeDate = null;

            await UpdateBeneficiary(beneficiary);

            return beneficiary.TopUpTotal;
        }
    }
}
