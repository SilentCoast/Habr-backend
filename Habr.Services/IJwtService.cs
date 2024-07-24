namespace Habr.Services
{
    public interface IJwtService
    {
        string GenerateToken(int userId);
    }
}
