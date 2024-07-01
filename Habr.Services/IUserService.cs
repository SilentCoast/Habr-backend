namespace Habr.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(string email, string password, string? name = null);
        Task<int> LogInAsync(string email, string password);
        Task ConfirmEmailAsync(string email, int userId);
    }
}
