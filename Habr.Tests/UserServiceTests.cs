using Habr.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Habr.Tests
{
    public class UserServiceTests : IAsyncLifetime
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
        public async Task GetName_ReturnsName()
        {
            var user = await _dObject.CreateUser();

            var getName = await _dObject.UserService.GetName(user.Id);

            Assert.Equal(user.Name, getName);
        }

        [Fact]
        public async Task CreateUser_ShouldCreateUser()
        {
            var name = "Test User";
            var email = "testuser@example.com";
            var password = "Password123!";

            await _dObject.UserService.CreateUser(email, password, name);
            var user = await _dObject.Context.Users.SingleOrDefaultAsync(u => u.Email == email);

            Assert.NotNull(user);
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.False(string.IsNullOrEmpty(user.PasswordHash));
            Assert.False(string.IsNullOrEmpty(user.Salt));
        }

        [Fact]
        public async Task CreateUser_InvalidEmail_ShouldThrowArgumentException()
        {
            var name = "Test User";
            var email = "invalid-email";
            var password = "Password123!";

            await Assert.ThrowsAsync<ArgumentException>(async () => await _dObject.UserService.CreateUser(email, password, name));
        }

        [Fact]
        public async Task CreateUser_DuplicateEmail_ShouldThrowArgumentException()
        {
            var name = "Test User";
            var email = "existinguser@example.com";
            var password = "Password123!";

            //Adds fine
            await _dObject.UserService.CreateUser(email, password, name);
            //Throws exception for duplicate email
            await Assert.ThrowsAsync<ArgumentException>(async () => await _dObject.UserService.CreateUser(email, password, name));
        }

        [Fact]
        public async Task LogIn_CorrectCredentials_ShouldReturnToken()
        {
            var name = "Test User";
            var email = "testuser@example.com";
            var password = "Password123!";
            await _dObject.UserService.CreateUser(email, password, name);

            var result = await _dObject.UserService.LogIn(email, password);

            Assert.Equal("mocked_token", result.AccessToken);
        }

        [Fact]
        public async Task LogIn_WrongCredentials_ShouldThrowLogInException()
        {
            var name = "Test User";
            var email = "testuser@example.com";
            var password = "Password123!";

            await _dObject.UserService.CreateUser(email, password, name);

            await Assert.ThrowsAsync<LogInException>(async () => await _dObject.UserService.LogIn(email, "wrong password"));
        }

        [Fact]
        public async Task ConfirmEmail_ShouldSetIsEmailConfirmed()
        {
            var email = "email@mail.com";
            await _dObject.UserService.CreateUser(email, "password");
            var user = await _dObject.Context.Users.FirstAsync();

            await _dObject.UserService.ConfirmEmail(email, user.Id);

            Assert.True(user.IsEmailConfirmed);
        }

        [Fact]
        public async Task ConfirmEmail_UserDontExist_ShouldThrowArgumentException()
        {
            var email = "email@mail.com";
            var nonExistentUserId = -1;

            await Assert.ThrowsAsync<ArgumentException>(async () => await _dObject.UserService.ConfirmEmail(email, nonExistentUserId));
        }
    }
}