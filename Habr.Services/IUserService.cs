using Habr.DataAccess.DTOs;

namespace Habr.Services
{
    public interface IUserService
    {
        Task<string> GetName(int userId, CancellationToken cancellationToken = default);
        Task CreateUser(string email, string password, string? name = null, CancellationToken cancellationToken = default);
        Task<TokensDto> LogIn(string email, string password, CancellationToken cancellationToken = default);
        Task ConfirmEmail(string email, int userId, CancellationToken cancellationToken = default);
    }
}
