using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> CheckUserAsync(string uid)
        {
            return await _userRepository.CheckUserAsync(uid);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(int userId, User user)
        {
            return await _userRepository.UpdateUserAsync(userId, user);
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }
    }
}