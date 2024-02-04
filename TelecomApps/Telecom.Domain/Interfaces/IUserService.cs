using Telecom.Domain.Models;

namespace Telecom.Domain.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUserAsync();
    }
}