using Habr.DataAccess.Entities;

namespace Habr.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(string name, string email, string password);

        /// <returns>If valid credentials provided: (true, <see cref="User"/> object corresponding to credentials)
        /// <para>Otherwise: (false, null)</para>
        /// </returns>
        Task<(bool, User?)> ValidateUserAsync(string email, string password);
    }
}
