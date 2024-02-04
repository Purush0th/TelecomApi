using Microsoft.EntityFrameworkCore;
using Telecom.Domain.Models;

namespace Telecom.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

            DbInitializer.Initialize(this);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<TopUpTransaction> TopUpTransactions { get; set; }

    }
}
