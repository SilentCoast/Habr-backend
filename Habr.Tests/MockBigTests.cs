using Habr.DataAccess;
using Habr.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Habr.Tests
{
    /// <summary>
    /// Emulate a lot of tests in big project
    /// Just for comparison purposes
    /// </summary>
    [Collection("Sequential")]
    public class MockBigTests : IAsyncLifetime
    {
        private DataContext _context;
        private DataContext _secondContext;
        private ServiceProvider _serviceProvider;
        private IPostService _postService;
        private IUserService _userService;
        private ICommentService _commentService;

        public async Task InitializeAsync()
        {
            _serviceProvider = Configurator.ConfigureServiceProvider();

            _context = _serviceProvider.GetRequiredService<DataContext>();
            _postService = _serviceProvider.GetRequiredService<IPostService>();
            _userService = _serviceProvider.GetRequiredService<IUserService>();
            _commentService = _serviceProvider.GetRequiredService<ICommentService>();

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
        public async Task MockTest1()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest2()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest3()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest4()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest5()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest6()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest7()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest8()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest9()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest10()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest11()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest12()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest13()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest14()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest15()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest16()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest17()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest18()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest19()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest20()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest21()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest22()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest23()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest24()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest25()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest26()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest27()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest28()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest29()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest30()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest31()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest32()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest33()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest34()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest35()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest36()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest37()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest38()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest39()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest40()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest41()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest42()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest43()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest44()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest45()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest46()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest47()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest48()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest49()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest50()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest51()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest52()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest53()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest54()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest55()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest56()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest57()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest58()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest59()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest60()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest61()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest62()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest63()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest64()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest65()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest66()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest67()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest68()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest69()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest70()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest71()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest72()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest73()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest74()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest75()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest76()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest77()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest78()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest79()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest80()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest81()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest82()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest83()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest84()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest85()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest86()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest87()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest88()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest89()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest90()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest91()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest92()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest93()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest94()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest95()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest96()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest97()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest98()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest99()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest100()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest101()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest102()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest103()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest104()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest105()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest106()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest107()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest108()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest109()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest110()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest111()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest112()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest113()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest114()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest115()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest116()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest117()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest118()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest119()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest120()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest121()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest122()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest123()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest124()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest125()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest126()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest127()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest128()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest129()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest130()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest131()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest132()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest133()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest134()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest135()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest136()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest137()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest138()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest139()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest140()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest141()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest142()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest143()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest144()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest145()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest146()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest147()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest148()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest149()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest150()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest151()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest152()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest153()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest154()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest155()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest156()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest157()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest158()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest159()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest160()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest161()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest162()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest163()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest164()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest165()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest166()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest167()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest168()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest169()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest170()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest171()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest172()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest173()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest174()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest175()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest176()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest177()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest178()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest179()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest180()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest181()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest182()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest183()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest184()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest185()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest186()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest187()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest188()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest189()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest190()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest191()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest192()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest193()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest194()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest195()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest196()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest197()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest198()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest199()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest200()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest201()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest202()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest203()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest204()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest205()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest206()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest207()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest208()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest209()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest210()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest211()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest212()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest213()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest214()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest215()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest216()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest217()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest218()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest219()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest220()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest221()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest222()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest223()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest224()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest225()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest226()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest227()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest228()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest229()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest230()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest231()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest232()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest233()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest234()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest235()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest236()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest237()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest238()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest239()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest240()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest241()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest242()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest243()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest244()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest245()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest246()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest247()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest248()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest249()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest250()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest251()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest252()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest253()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest254()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest255()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest256()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest257()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest258()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest259()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest260()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest261()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest262()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest263()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest264()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest265()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest266()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest267()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest268()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest269()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest270()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest271()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest272()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest273()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest274()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest275()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest276()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest277()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest278()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest279()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest280()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest281()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest282()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest283()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest284()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest285()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest286()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest287()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest288()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest289()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest290()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest291()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest292()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest293()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest294()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest295()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest296()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest297()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest298()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest299()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest300()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest301()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest302()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest303()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest304()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest305()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest306()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest307()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest308()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest309()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest310()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest311()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest312()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest313()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest314()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest315()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest316()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest317()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest318()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest319()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest320()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest321()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest322()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest323()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest324()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest325()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest326()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest327()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest328()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest329()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest330()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest331()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest332()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest333()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest334()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest335()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest336()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest337()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest338()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest339()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest340()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest341()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest342()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest343()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest344()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest345()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest346()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest347()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest348()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest349()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest350()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest351()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest352()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest353()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest354()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest355()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest356()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest357()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest358()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest359()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest360()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest361()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest362()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest363()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest364()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest365()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest366()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest367()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest368()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest369()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest370()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest371()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest372()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest373()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest374()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest375()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest376()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest377()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest378()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest379()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest380()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest381()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest382()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest383()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest384()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest385()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest386()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest387()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest388()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest389()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest390()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest391()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest392()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest393()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest394()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest395()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest396()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest397()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest398()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest399()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest400()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest401()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest402()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest403()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest404()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest405()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest406()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest407()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest408()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest409()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest410()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest411()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest412()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest413()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest414()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest415()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest416()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest417()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest418()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest419()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest420()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest421()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest422()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest423()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest424()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest425()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest426()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest427()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest428()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest429()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest430()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest431()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest432()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest433()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest434()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest435()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest436()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest437()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest438()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest439()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest440()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest441()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest442()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest443()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest444()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest445()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest446()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest447()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest448()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest449()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest450()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest451()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest452()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest453()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest454()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest455()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest456()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest457()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest458()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest459()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest460()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest461()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest462()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest463()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest464()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest465()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest466()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest467()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest468()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest469()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest470()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest471()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest472()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest473()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest474()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest475()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest476()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest477()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest478()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest479()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest480()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest481()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest482()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest483()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest484()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest485()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest486()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest487()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest488()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest489()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest490()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest491()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest492()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest493()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest494()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest495()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest496()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest497()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest498()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest499()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest500()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest501()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest502()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest503()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest504()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest505()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest506()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest507()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest508()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest509()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest510()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest511()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest512()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest513()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest514()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest515()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest516()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest517()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest518()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest519()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest520()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest521()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest522()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest523()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest524()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest525()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest526()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest527()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest528()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest529()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest530()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest531()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest532()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest533()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest534()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest535()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest536()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest537()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest538()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest539()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest540()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest541()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest542()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest543()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest544()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest545()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest546()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest547()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest548()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest549()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest550()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest551()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest552()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest553()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest554()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest555()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest556()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest557()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest558()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest559()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest560()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest561()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest562()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest563()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest564()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest565()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest566()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest567()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest568()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest569()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest570()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest571()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest572()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest573()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest574()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest575()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest576()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest577()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest578()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest579()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest580()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest581()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest582()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest583()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest584()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest585()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest586()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest587()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest588()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest589()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest590()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest591()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest592()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest593()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest594()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest595()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest596()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest597()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest598()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest599()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest600()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest601()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest602()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest603()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest604()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest605()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest606()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest607()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest608()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest609()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest610()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest611()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest612()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest613()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest614()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest615()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest616()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest617()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest618()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest619()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest620()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest621()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest622()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest623()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest624()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest625()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest626()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest627()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest628()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest629()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest630()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest631()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest632()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest633()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest634()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest635()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest636()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest637()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest638()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest639()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest640()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest641()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest642()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest643()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest644()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest645()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest646()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest647()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest648()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest649()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest650()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest651()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest652()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest653()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest654()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest655()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest656()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest657()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest658()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest659()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest660()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest661()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest662()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest663()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest664()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest665()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest666()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest667()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest668()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest669()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest670()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest671()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest672()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest673()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest674()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest675()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest676()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest677()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest678()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest679()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest680()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest681()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest682()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest683()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest684()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest685()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest686()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest687()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest688()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest689()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest690()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest691()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest692()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest693()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest694()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest695()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest696()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest697()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest698()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest699()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest700()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest701()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest702()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest703()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest704()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest705()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest706()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest707()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest708()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest709()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest710()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest711()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest712()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest713()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest714()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest715()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest716()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest717()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest718()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest719()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest720()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest721()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest722()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest723()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest724()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest725()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest726()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest727()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest728()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest729()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest730()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest731()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest732()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest733()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest734()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest735()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest736()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest737()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest738()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest739()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest740()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest741()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest742()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest743()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest744()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest745()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest746()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest747()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest748()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest749()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest750()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest751()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest752()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest753()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest754()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest755()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest756()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest757()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest758()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest759()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest760()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest761()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest762()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest763()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest764()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest765()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest766()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest767()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest768()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest769()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest770()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest771()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest772()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest773()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest774()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest775()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest776()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest777()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest778()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest779()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest780()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest781()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest782()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest783()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest784()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest785()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest786()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest787()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest788()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest789()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest790()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest791()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest792()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest793()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest794()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest795()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest796()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest797()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest798()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest799()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest800()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest801()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest802()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest803()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest804()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest805()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest806()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest807()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest808()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest809()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest810()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest811()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest812()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest813()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest814()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest815()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest816()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest817()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest818()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest819()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest820()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest821()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest822()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest823()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest824()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest825()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest826()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest827()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest828()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest829()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest830()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest831()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest832()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest833()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest834()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest835()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest836()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest837()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest838()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest839()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest840()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest841()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest842()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest843()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest844()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest845()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest846()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest847()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest848()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest849()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest850()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest851()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest852()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest853()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest854()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest855()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest856()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest857()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest858()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest859()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest860()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest861()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest862()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest863()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest864()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest865()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest866()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest867()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest868()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest869()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest870()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest871()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest872()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest873()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest874()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest875()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest876()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest877()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest878()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest879()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest880()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest881()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest882()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest883()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest884()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest885()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest886()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest887()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest888()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest889()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest890()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest891()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest892()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest893()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest894()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest895()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest896()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest897()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest898()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest899()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest900()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest901()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest902()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest903()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest904()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest905()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest906()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest907()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest908()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest909()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest910()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest911()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest912()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest913()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest914()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest915()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest916()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest917()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest918()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest919()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest920()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest921()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest922()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest923()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest924()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest925()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest926()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest927()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest928()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest929()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest930()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest931()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest932()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest933()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest934()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest935()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest936()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest937()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest938()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest939()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest940()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest941()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest942()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest943()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest944()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest945()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest946()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest947()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest948()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest949()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest950()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest951()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest952()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest953()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest954()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest955()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest956()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest957()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest958()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest959()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest960()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest961()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest962()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest963()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest964()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest965()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest966()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest967()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest968()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest969()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest970()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest971()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest972()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest973()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest974()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest975()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest976()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest977()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest978()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest979()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest980()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest981()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest982()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest983()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest984()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest985()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest986()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest987()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest988()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest989()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest990()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest991()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest992()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest993()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest994()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest995()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest996()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest997()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest998()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest999()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }

        [Fact]
        public async Task MockTest1000()
        {
            await _userService.CreateUser("email@mail.com", "pas");
            var user = await _context.Users.FirstAsync();
            await _postService.AddPost("test", "test", user.Id);
            var post = await _context.Posts.FirstAsync();
            Assert.NotNull(post);
        }
    }
}
