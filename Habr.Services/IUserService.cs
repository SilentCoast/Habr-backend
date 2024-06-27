namespace Habr.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(string name, string email, string password);
        Task<int> LogIn(string email, string password);
        Task ConfirmEmailAsync(string enail, int userId);
    }
}
