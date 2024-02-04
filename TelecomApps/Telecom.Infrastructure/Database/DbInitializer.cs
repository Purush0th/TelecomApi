using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telecom.Domain.Models;

namespace Telecom.Infrastructure.Database
{
    // Infrastructure/Data/DbInitializer.cs
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated(); // Ensure that the database is created

            // Check if there is already data in the database
            if (context.Users.Any())
            {
                return; // Database has been seeded
            }

            // Seed initial data
            var user =
               new User { Email = "admin@example.com", Username = "admin", FirstName = "Adminstrator", LastName = "User", Password = "admin", IsVerified = true };


            var account = new Account { User = user, AvailableTopUpCredit = 3000, TotalTopUpCredit = 3000 };

            var beneficary1 = new Beneficiary { Account = account, NickName = "Guest 1", PhoneNumber = "+971-50-1234567" };

            var beneficary2 = new Beneficiary { Account = account, NickName = "Guest 2", PhoneNumber = "+971-51-1234567" };

            context.Users.Add(user);
            context.Accounts.Add(account);
            context.Beneficiaries.Add(beneficary1);
            context.Beneficiaries.Add(beneficary2);

            context.SaveChanges();
        }
    }
}
