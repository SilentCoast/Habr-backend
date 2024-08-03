using Habr.DataAccess.Entities;

namespace Habr.DataAccess.DTOs
{
    public class TokensDTO
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
