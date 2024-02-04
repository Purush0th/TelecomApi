using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telecom.Domain.Interfaces.Repositories;
using Telecom.Domain.Models;
using Telecom.Infrastructure.Database;

namespace Telecom.Infrastructure.Repositories
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly AppDbContext _dbContext;
        private DbSet<Beneficiary> _beneficiaryEntities;
        private readonly DbSet<Account> _accountEntities;

        public BeneficiaryRepository(AppDbContext context)
        {
            _dbContext = context;
            _beneficiaryEntities = context.Beneficiaries;
            _accountEntities = context.Accounts;

        }
        public async Task<Beneficiary> CreateBeneficiary(Beneficiary beneficiary, int accountId)
        {
            var account = await _accountEntities.FirstOrDefaultAsync(i => i.Id == accountId);
            beneficiary.Account = account;
            _beneficiaryEntities.Add(beneficiary);

            _dbContext.SaveChanges();

            return await _beneficiaryEntities.FindAsync(beneficiary.Id);
        }

        public async Task<int> GetBeneficiariesCoundForAccount(int accountId)
        {
            return await _beneficiaryEntities.Where(i => i.AccountId == accountId).CountAsync();
        }

        public async Task<IEnumerable<Beneficiary>> GetBeneficiariesOfAccountAsync(int accountId)
        {
            return await _beneficiaryEntities.Where(i => i.AccountId == accountId).ToListAsync();
        }

        public async Task<Beneficiary> GetBeneficiaryById(int id)
        {
            return await _beneficiaryEntities
                .Include(i => i.Account)
                .ThenInclude(t => t.User)
                .FirstOrDefaultAsync(i => i.Id.Equals(id));
        }

        public async Task<bool> UpdateBeneficiaryRecharge(int beneficiaryId, int rechargeAmount, DateTime date)
        {
            var entity = await _beneficiaryEntities.FindAsync(beneficiaryId);

            if (entity == null)
                return false;

            entity.LastRechargeAmount = rechargeAmount;
            entity.TopUpTotal += rechargeAmount;
            entity.LastRechargeDate = date;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateBeneficiary(Beneficiary beneficiary)
        {
            var entity = await _beneficiaryEntities.FindAsync(beneficiary.Id);

            entity.PhoneNumber = beneficiary.PhoneNumber;
            entity.NickName = beneficiary.NickName;
            entity.LastRechargeAmount = beneficiary.LastRechargeAmount;
            entity.LastRechargeDate = beneficiary.LastRechargeDate;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
