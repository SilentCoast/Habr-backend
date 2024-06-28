using Habr.DataAccess;
using Habr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Habr.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private DataContext _context;
        private DataContext _secondContext;
        private ServiceProvider _serviceProvider;
        private IUserService _userService;

        [TestInitialize]
        public async Task InitializeAsync()
        {
            _serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options => Configurator.ConfigureDbContextOptions(options))
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

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            Configurator.ConfigureDbContextOptions(optionsBuilder);
            
            _secondContext = new DataContext(optionsBuilder.Options);

            await _context.Database.MigrateAsync();
        }

        [TestCleanup]
        public async Task Cleanup()
        {
            await _context.Users.ExecuteDeleteAsync();
            await _context.Posts.ExecuteDeleteAsync();
            await _context.Comments.ExecuteDeleteAsync();

            await _context.DisposeAsync();
            await _secondContext.DisposeAsync();
            await _serviceProvider.DisposeAsync();
        }

        [TestMethod]
        public async Task CreateUserAsync_ShouldCreateUser()
        {
            var name = "Test User";
            var email = "testuser@example.com";
            var password = "Password123!";

            await _userService.CreateUserAsync(email, password, name);

            var user = await _secondContext.Users.SingleOrDefaultAsync(u => u.Email == email);
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
            var name = "Test User";
            var email = "invalid-email";
            var password = "Password123!";

            await _userService.CreateUserAsync(email, password,name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreateUserAsync_DuplicateEmail_ShouldThrowArgumentException()
        {
            var name = "Test User";
            var email = "existinguser@example.com";
            var password = "Password123!";

            //Adds fine
            await _userService.CreateUserAsync(email, password, name);
            //Throws exception for duplicate email
            await _userService.CreateUserAsync(email, password, name);
        }

        [TestMethod]
        public async Task LogIn_CredentialsAreCorrect_ShouldReturnUserId()
        {
            var name = "Test User";
            var email = "testuser@example.com";
            var password = "Password123!";

            await _userService.CreateUserAsync(email, password, name);

            var user = _secondContext.Users.First();

            var result = await _userService.LogIn(email, password);

            Assert.AreEqual(user.Id, result, "The returned user ID should match the logged-in user's ID.");
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public async Task LogIn_WrongCredentials_ShouldThrowUnauthorizedAccessException()
        {
            var name = "Test User";
            var email = "testuser@example.com";
            var password = "Password123!";

            await _userService.CreateUserAsync(email, password, name);

            await _userService.LogIn(email, "wrong password");
        }

        [TestMethod]
        public async Task ConfirmEmailAsync_ShouldSetIsEmailConfirmed()
        {
            string email = "email@mail.com";

            await _userService.CreateUserAsync(email, "password");

            var user = _context.Users.First();

            await _userService.ConfirmEmailAsync(email, user.Id);

            user = _secondContext.Users.First();

            Assert.IsTrue(user.IsEmailConfirmed);
        }
    }
}