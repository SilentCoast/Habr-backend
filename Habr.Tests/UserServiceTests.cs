using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Habr.Services;
using Habr.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Habr.Tests
{
    public class UserServiceTests : IAsyncLifetime
    {
        private DataContext _context;
        private ServiceProvider _serviceProvider;
        private IUserService _userService;

        public async Task InitializeAsync()
        {
            _serviceProvider = Configurator.ConfigureServiceProvider();

            _context = _serviceProvider.GetRequiredService<DataContext>();
            _userService = _serviceProvider.GetRequiredService<IUserService>();
        }

        public async Task DisposeAsync()
        {
            await _context.Database.EnsureDeletedAsync();

            await _context.DisposeAsync();
            await _serviceProvider.DisposeAsync();
        }

        [Fact]
        public async Task GetName_ReturnsName()
        {
            var user = await CreateUser();

            var getName = await _userService.GetName(user.Id);

            Assert.Equal(user.Name, getName);
        }

        [Fact]
        public async Task CreateUser_ShouldCreateUser()
        {
            var name = "Test User";
            var email = "testuser@example.com";
            var password = "Password123!";

            await _userService.CreateUser(email, password, name);
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

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

            await Assert.ThrowsAsync<ArgumentException>(async () => await _userService.CreateUser(email, password, name));
        }

        [Fact]
        public async Task CreateUser_DuplicateEmail_ShouldThrowArgumentException()
        {
            var name = "Test User";
            var email = "existinguser@example.com";
            var password = "Password123!";

            //Adds fine
            await _userService.CreateUser(email, password, name);
            //Throws exception for duplicate email
            await Assert.ThrowsAsync<ArgumentException>(async () => await _userService.CreateUser(email, password, name));
        }

        [Fact]
        public async Task LogIn_CorrectCredentials_ShouldReturnToken()
        {
            var name = "Test User";
            var email = "testuser@example.com";
            var password = "Password123!";
            await _userService.CreateUser(email, password, name);

            var result = await _userService.LogIn(email, password);

            Assert.Equal("mocked_token", result);
        }

        [Fact]
        public async Task LogIn_WrongCredentials_ShouldThrowLogInException()
        {
            var name = "Test User";
            var email = "testuser@example.com";
            var password = "Password123!";

            await _userService.CreateUser(email, password, name);

            await Assert.ThrowsAsync<LogInException>(async () => await _userService.LogIn(email, "wrong password"));
        }

        [Fact]
        public async Task ConfirmEmail_ShouldSetIsEmailConfirmed()
        {
            var email = "email@mail.com";
            await _userService.CreateUser(email, "password");
            var user = await _context.Users.FirstAsync();

            await _userService.ConfirmEmail(email, user.Id);

            Assert.True(user.IsEmailConfirmed);
        }

        [Fact]
        public async Task ConfirmEmail_UserDontExist_ShouldThrowArgumentException()
        {
            var email = "email@mail.com";
            var nonExistentUserId = -1;

            await Assert.ThrowsAsync<ArgumentException>(async () => await _userService.ConfirmEmail(email, nonExistentUserId));
        }

        private async Task<User> CreateUser()
        {
            await _userService.CreateUser("john.doe@example.com", "password");
            return await _context.Users.FirstAsync();
        }
    }
}