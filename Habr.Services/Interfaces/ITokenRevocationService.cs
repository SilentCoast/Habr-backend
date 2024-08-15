using System.Security.Claims;

namespace Habr.Services.Interfaces
{
    public interface ITokenRevocationService
    {
        Task RevokeAllUserTokens(int userId, CancellationToken cancellationToken = default);
        Task CheckTokenRevocation(ClaimsPrincipal principal, CancellationToken cancellationToken = default);
    }
}