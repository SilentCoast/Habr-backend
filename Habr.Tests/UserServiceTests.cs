using Habr.DataAccess;
using Habr.Services;
using Habr.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Habr.Tests
{
    //TODO: cover more cases
    [Collection("Sequential")]
    public class UserServiceTests : IAsyncLifetime
    {
        private DataContext _context;
        private DataContext _secondContext;
        private ServiceProvider _serviceProvider;
        private IUserService _userService;

        public async Task InitializeAsync()
        {
            _serviceProvider = Configurator.ConfigureServiceProvider();

            _context = _serviceProvider.GetRequiredService<DataContext>();
            _userService = _serviceProvider.GetRequiredService<IUserService>();

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            Configurator.ConfigureDbContextOptions(optionsBuilder);

            _secondContext = new DataContext(optionsBuilder.Options);

            await _context.Database.MigrateAsync();
        }

        public async Task DisposeAsync()
        {
            await _context.Users.ExecuteDeleteAsync();
            await _context.Posts.ExecuteDeleteAsync();
            await _context.Comments.ExecuteDeleteAsync();

            await _context.DisposeAsync();
            await _secondContext.DisposeAsync();
            await _serviceProvider.DisposeAsync();
        }

        [Fact]
        public async Task CreateUser_ShouldCreateUser()
        {
            var name = "Test User";
            var email = "testuser@example.com";
            var password = "Password123!";

            await _userService.CreateUser(email, password, name);

            var user = await _secondContext.Users.SingleOrDefaultAsync(u => u.Email == email);
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
        public async Task LogIn_CredentialsAreCorrect_ShouldReturnUserId()
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

            user = await _secondContext.Users.FirstAsync();

            Assert.True(user.IsEmailConfirmed);
        }
    }
}