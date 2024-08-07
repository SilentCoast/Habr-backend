namespace Habr.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateAccessToken(int userId);
        Task<string> GenerateRefreshToken(int userId, CancellationToken cancellationToken = default);
        Task<string> RefreshAccessToken(string refreshToken, CancellationToken cancellationToken = default);
    }
}
