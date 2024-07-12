namespace Habr.Services
{
    public interface IUserService
    {
        Task<string> GetName(int userId);
        Task CreateUser(string email, string password, string? name = null, CancellationToken cancellationToken = default);
        Task<int> LogIn(string email, string password, CancellationToken cancellationToken = default);
        Task ConfirmEmail(string email, int userId, CancellationToken cancellationToken = default);
    }
}
