using Habr.DataAccess.Entities;

namespace Habr.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAccessToken(int userId);
        Task<RefreshToken> GenerateRefreshToken(int userId, CancellationToken cancellationToken = default);
        Task ValidateRefreshToken(RefreshToken token, CancellationToken cancellationToken = default);
    }
}
