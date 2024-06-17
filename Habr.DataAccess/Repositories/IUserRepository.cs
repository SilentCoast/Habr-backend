using Habr.DataAccess.Entities;

namespace Habr.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
    }
}
