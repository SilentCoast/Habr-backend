using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
            if (!IsValidEmail(email))
            {
                throw new ArgumentException("Invalid email format.");
            }

            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                throw new ArgumentException("email is already taken");
            }

            string salt = _passwordHasher.GenerateSalt();
            string hashedPassword = _passwordHasher.HashPassword(password, salt);


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

        /// <summary>
        /// TODO: clarification for mentor:
        /// Now returning user.Id since it acts as a token
        /// </summary>
        public async Task<int> LogIn(string email, string password)
        {
            //TODO: clarification for mentor:
            // Here throwing two different exceptions with distinct messages, since blueprint was specified in the Jira.
            // Should these blueprints be followed precisely?
            // Otherwise, I would have just thrown the same exception with the message "Wrong credentials."

            User user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email) ?? throw new UnauthorizedAccessException("The email is incorrect");

            string hashedPassword = _passwordHasher.HashPassword(password, user.Salt);
            if (hashedPassword != user.PasswordHash)
            {
                throw new UnauthorizedAccessException("Wrong credentials");
            }

            return user.Id;
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
            return emailRegex.IsMatch(email);
        }
    }
}
