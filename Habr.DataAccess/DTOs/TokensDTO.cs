using Habr.DataAccess.Entities;

namespace Habr.DataAccess.DTOs
{
    public class TokensDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
