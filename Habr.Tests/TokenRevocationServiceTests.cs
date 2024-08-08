using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Habr.Tests
{
    public class TokenRevocationServiceTests : IAsyncLifetime
    {
        private DependencyObject _dObject;

        public async Task InitializeAsync()
        {
            _dObject = new DependencyObject();
            await _dObject.Initilize();
        }

        public async Task DisposeAsync()
        {
            await _dObject.Dispose();
        }

        [Fact]
        public async Task RevokeAllUserTokens_ShouldRevokeAllTokens()
        {
            var user = await CreateUser();
            var expectedTokenVersion = user.TokenVersion + 1;
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(1),
                RevokedAt = null,
                UserId = user.Id
            };
            _dObject.Context.RefreshTokens.Add(refreshToken);
            await _dObject.Context.SaveChangesAsync();

            await _dObject.TokenRevocationService.RevokeAllUserTokens(user.Id);

            Assert.NotNull(refreshToken.RevokedAt);
            Assert.Equal(expectedTokenVersion, user.TokenVersion);
        }

        private async Task<User> CreateUser(string email = "john.doe@example.com", string password = "password")
        {
            await _dObject.UserService.CreateUser(email, password);
            return await _dObject.Context.Users.FirstAsync();
        }
    }
}
