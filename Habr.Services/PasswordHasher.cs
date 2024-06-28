using System.Security.Cryptography;
using System.Text;

namespace Habr.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                var builder = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string GenerateSalt()
        {
            var saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
    }
}
