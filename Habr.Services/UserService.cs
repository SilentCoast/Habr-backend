using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Habr.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(DataContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task CreateUserAsync(string name, string email, string password)
        {
            string salt = _passwordHasher.GenerateSalt();
            string hashedPassword = _passwordHasher.HashPassword(password, salt);

            if (await _context.Users.SingleOrDefaultAsync(u => u.Email == email) != null)
            {
                throw new Exception("User with that email already exists");
            }

            User user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = hashedPassword,
                Salt = salt,
                CreatedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task LogIn(string email, string password)
        {
            User user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                string hashedPassword = _passwordHasher.HashPassword(password, user.Salt);
                if (hashedPassword == user.PasswordHash)
                {
                    return;
                }
            }

            throw new UnauthorizedAccessException("Wrong Credentials");
        }
    }
}
