using Habr.DataAccess.Entities;

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
            var user = await _dObject.CreateUser();
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
    }
}
