﻿using Habr.DataAccess;
using Habr.DataAccess.Constraints;
using Habr.DataAccess.Entities;
using Habr.Services.Exceptions;
using Habr.Services.Resources;
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

        public async Task CreateUser(string email, string password, string? name = null, CancellationToken cancellationToken = default)
        {
            if (email.Length > ConstraintValue.UserEmailMaxLength)
            {
                throw new ArgumentException(string.Format(ExceptionMessageGeneric.ValueTooLongMaxLengthIs, nameof(email), ConstraintValue.UserEmailMaxLength));
            }

            if (!IsValidEmail(email))
            {
                throw new ArgumentException(ExceptionMessage.InvalidEmail);
            }

            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                throw new ArgumentException(ExceptionMessage.EmailTaken);
            }

            if (name == null)
            {
                name = email.Split('@')[0];
            }

            if (name.Length > ConstraintValue.UserNameMaxLength)
            {
                throw new ArgumentException(string.Format(ExceptionMessageGeneric.ValueTooLongMaxLengthIs, nameof(name), ConstraintValue.UserNameMaxLength));
            }

            var salt = _passwordHasher.GenerateSalt();
            var hashedPassword = _passwordHasher.HashPassword(password, salt);

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = hashedPassword,
                Salt = salt,
                CreatedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> LogIn(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken)
                ?? throw new LogInException(ExceptionMessage.EmailIncorrect);

            var hashedPassword = _passwordHasher.HashPassword(password, user.Salt);
            if (hashedPassword != user.PasswordHash)
            {
                throw new LogInException(ExceptionMessage.WrongCredentials);
            }

            return user.Id;
        }

        public async Task ConfirmEmail(string email, int userId, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.SingleOrDefaultAsync(p => p.Id == userId, cancellationToken);
            if (user == null)
            {
                throw new ArgumentException(ExceptionMessage.UserDoesntExist);
            }

            //add actual confirmation mechanism
            if (email is not string)
            {
                throw new EmailConfirmationException();
            }

            user.IsEmailConfirmed = true;

            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
            return emailRegex.IsMatch(email);
        }
    }
}
