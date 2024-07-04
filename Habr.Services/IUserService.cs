namespace Habr.Services
{
    public interface IUserService
    {
        Task CreateUser(string email, string password, string? name = null);
        Task<int> LogIn(string email, string password);
        Task ConfirmEmail(string email, int userId);
    }
}
