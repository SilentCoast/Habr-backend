﻿using Habr.DataAccess;
using Habr.DataAccess.Constraints;
using Habr.DataAccess.DTOs;
using Habr.DataAccess.Entities;
using Habr.DataAccess.Enums;
using Habr.Services.Exceptions;
using Habr.Services.Interfaces;
using Habr.Services.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Habr.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<UserService> _logger;
        private readonly IJwtService _jwtService;

        public UserService(DataContext context, IPasswordHasher passwordHasher, ILogger<UserService> logger, IJwtService jwtService)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _jwtService = jwtService;
        }

        public async Task<string> GetName(int userId, CancellationToken cancellationToken = default)
        {
            var name = await _context.Users.Where(p => p.Id == userId).Select(p => p.Name).SingleOrDefaultAsync(cancellationToken);

            return name ?? throw new ArgumentException(ExceptionMessage.UserDoesntExist);
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

            if (await _context.Users.AnyAsync(u => u.Email == email, cancellationToken))
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

            var roleId = await _context.Roles.Where(p => p.RoleType == RoleType.User).Select(p => p.Id).SingleAsync(cancellationToken);

            var user = new User
            {
                Name = name,
                Email = email,
                PasswordHash = hashedPassword,
                Salt = salt,
                CreatedAt = DateTime.UtcNow,
                RoleId = roleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User registered: {email}");
        }

        public async Task<TokensDto> LogIn(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken)
                ?? throw new LogInException(ExceptionMessage.EmailIncorrect);

            var hashedPassword = _passwordHasher.HashPassword(password, user.Salt);
            if (hashedPassword != user.PasswordHash)
            {
                throw new LogInException(ExceptionMessage.WrongCredentials);
            }

            var accesToken = await _jwtService.GenerateAccessToken(user.Id, cancellationToken);
            var refreshToken = await _jwtService.GenerateRefreshToken(user.Id, cancellationToken);

            _logger.LogInformation($"User logged in: {email}");

            return new TokensDto { AccessToken = accesToken, RefreshToken = refreshToken };
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

        private static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
            return emailRegex.IsMatch(email);
        }
    }
}
