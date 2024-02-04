using Microsoft.EntityFrameworkCore;
using Telecom.Domain.Interfaces.Repositories;
using Telecom.Domain.Models;
using Telecom.Infrastructure.Database;

namespace Telecom.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSet<User> _users;
        public UserRepository(AppDbContext dbContext)
        {
            _users = dbContext.Users;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _users.ToListAsync();
        }
    }
}
