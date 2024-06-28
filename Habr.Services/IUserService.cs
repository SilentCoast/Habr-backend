namespace Habr.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(string email, string password, string? name = null);
        Task<int> LogIn(string email, string password);
        Task ConfirmEmailAsync(string enail, int userId);
    }
}
