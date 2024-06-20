namespace Habr.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, string salt);
        string GenerateSalt();
    }
}
