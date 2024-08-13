using Habr.DataAccess;
using Habr.Services.Interfaces;
using Habr.Services.Resources;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Habr.Services
{
    public class TokenRevocationService : ITokenRevocationService
    {
        private readonly DataContext _context;

        public TokenRevocationService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Updates valid TokenVersion for AccessTokens and sets RevokedAt in RefreshTokens
        /// </summary>
        public async Task RevokeAllUserTokens(int userId, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new ArgumentException(ExceptionMessage.UserDoesntExist);
            }

            user.TokenVersion++;
            _context.Users.Update(user);

            var refreshTokens = await _context.RefreshTokens.Where(p => p.UserId == userId).ToListAsync(cancellationToken);
            foreach (var refreshToken in refreshTokens)
            {
                refreshToken.RevokedAt = DateTime.UtcNow;
            }
            _context.RefreshTokens.UpdateRange(refreshTokens);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CheckTokenRevocation(ClaimsPrincipal principal, CancellationToken cancellationToken = default)
        {
            var userId = int.Parse(principal.FindFirst("userId").Value);
            var tokenVersion = int.Parse(principal.FindFirst("tokenVersion").Value);

            var user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                if (user.TokenVersion == tokenVersion)
                {
                    return;
                }
            }

            throw new UnauthorizedAccessException(ExceptionMessage.TokenRevoked);
        }
    }
}
