using Telecom.Domain.Interfaces;
using Telecom.Domain.Interfaces.Repositories;
using Telecom.Domain.Models;

namespace Telecom.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepos;
        public UserService(IUserRepository userRepository)
        {
            _userRepos = userRepository;
        }



        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _userRepos.GetUsersAsync();
        }
    }
}
