using Habr.DataAccess.Entities;
using Habr.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace Habr.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }

        public async Task CreateUserAsync(string name, string email, string password)
        {
            try
            {
                string salt = _passwordHasher.GenerateSalt();
                string hashedPassword = _passwordHasher.HashPassword(password, salt);

                if (await _userRepository.GetUserByEmailAsync(email) != null)
                {
                    throw new Exception("User with that email already exists");
                }

                User user = new User
                {
                    Name = name,
                    Email = email,
                    PasswordHash = hashedPassword,
                    Salt = salt
                };

                await _userRepository.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            _logger.LogInformation($"User {name} added");
        }

        public async Task<(bool, User?)> ValidateUserAsync(string email, string password)
        {
            User user = await _userRepository.GetUserByEmailAsync(email);

            if (user != null)
            {
                string hashedPassword = _passwordHasher.HashPassword(password, user.Salt);
                if (hashedPassword == user.PasswordHash)
                {
                    return (true, user);
                }
            }

            return (false, null);
        }
    }
}
