using Telecom.Domain.Models;

namespace Telecom.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
