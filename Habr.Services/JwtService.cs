using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Habr.Services.Interfaces;
using Habr.Services.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Habr.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly DataContext _context;

        public JwtService(IOptions<JwtSettings> jwtSettings, DataContext context)
        {
            _jwtSettings = jwtSettings.Value;
            _context = context;
        }

        public async Task<string> GenerateAccessToken(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.Include(p => p.Role).SingleAsync(p => p.Id == userId, cancellationToken);

            var claims = new[]
            {
                new Claim("userId", userId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.RoleType.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.AccessTokenLifetimeHours),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(int userId, CancellationToken cancellationToken = default)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenLifetimeDays),
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync(cancellationToken);

            return refreshToken.Token;
        }

        /// <summary>
        /// validates refreshToken and generates AccessToken
        /// </summary>
        public async Task<string> RefreshAccessToken(string refreshToken, CancellationToken cancellationToken = default)
        {
            var tokenFromDb = await _context.RefreshTokens.FirstOrDefaultAsync(p => p.Token == refreshToken, cancellationToken);
            if (tokenFromDb != null)
            {
                if (tokenFromDb.RevokedAt == null)
                {
                    if (tokenFromDb.ExpiresAt > DateTime.UtcNow)
                    {
                        return await GenerateAccessToken(tokenFromDb.UserId);
                    }
                }
            }
            throw new SecurityTokenValidationException(ExceptionMessage.TokenValidationFailed);
        }
    }
}
