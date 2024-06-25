using Habr.DataAccess;
using Habr.DataAccess.Entities;
using Habr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Habr.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private DataContext _context;
        private ServiceProvider _serviceProvider;
        private IUserService _userService;

        [TestInitialize]
        public void Initialize()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                })
                .AddScoped<IUserService, UserService>()
                .AddSingleton<IPasswordHasher, PasswordHasher>()
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.SetMinimumLevel(LogLevel.Information);
                })
                .BuildServiceProvider();

            _context = _serviceProvider.GetRequiredService<DataContext>();
            _userService = _serviceProvider.GetRequiredService<IUserService>();
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
            await _serviceProvider.DisposeAsync();
        }

        [TestMethod]
        public async Task CreateUserAsync_ShouldCreateUser()
        {
            string name = "Test User";
            string email = "testuser@example.com";
            string password = "Password123!";

            await _userService.CreateUserAsync(name, email, password);

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            Assert.IsNotNull(user, "User should be created and found in the database.");
            Assert.AreEqual(name, user.Name, "The user's name should be set correctly.");
            Assert.AreEqual(email, user.Email, "The user's email should be set correctly.");
            Assert.IsFalse(string.IsNullOrEmpty(user.PasswordHash), "The user's password hash should be set.");
            Assert.IsFalse(string.IsNullOrEmpty(user.Salt), "The user's salt should be set.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreateUserAsync_InvalidEmail_ShouldThrowArgumentException()
        {
            string name = "Test User";
            string email = "invalid-email";
            string password = "Password123!";

            await _userService.CreateUserAsync(name, email, password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreateUserAsync_DuplicateEmail_ShouldThrowArgumentException()
        {
            string name = "Test User";
            string email = "existinguser@example.com";
            string password = "Password123!";

            await _userService.CreateUserAsync(name, email, password);

            await _userService.CreateUserAsync(name, email, password);
        }

        [TestMethod]
        public async Task LogIn_CredentialsAreCorrect_ShouldReturnUserId()
        {
            string name = "Test User";
            string email = "testuser@example.com";
            string password = "Password123!";

            await _userService.CreateUserAsync(name,email, password);

            var user = _context.Users.First();

            var result = await _userService.LogIn(email, password);

            Assert.AreEqual(user.Id, result, "The returned user ID should match the logged-in user's ID.");
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task LogIn_WrongCredentials_ShouldThrowUnauthorizedAccessException()
        {
            string name = "Test User";
            string email = "testuser@example.com";
            string password = "Password123!";

            await _userService.CreateUserAsync(name, email, password);

            await _userService.LogIn(email, "wrong password");
        }
    }
}