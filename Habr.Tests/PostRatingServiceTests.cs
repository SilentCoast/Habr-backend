using Microsoft.EntityFrameworkCore;

namespace Habr.Tests
{
    public class PostRatingServiceTests : IAsyncLifetime
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
        public async Task AddOrUpdatePostRating_NewEntry_ShouldAdd()
        {
            var user = await _dObject.CreateUser();
            var post = await _dObject.CreatePost(user.Id, true);
            var ratingStars = 3;
            var description = "some description";

            await _dObject.PostRatingService.AddOrUpdatePostRating(ratingStars, post.Id, user.Id, description);

            var ratingObj = await _dObject.Context.PostRatings.Where(p => p.PostId == post.Id && p.UserId == user.Id).FirstAsync();
            Assert.Equal(ratingStars, ratingObj.RatingStars);
            Assert.Equal(description, ratingObj.Description);
        }
        [Fact]
        public async Task AddOrUpdatePostRating_UpdateEntry_ShouldUpdate()
        {
            var user = await _dObject.CreateUser();
            var post = await _dObject.CreatePost(user.Id, true);
            var ratingStars = 3;
            var ratingStarsUpdated = 5;
            var description = "some description";
            var descriptionUpdated = "updated description";

            //add
            await _dObject.PostRatingService.AddOrUpdatePostRating(ratingStars, post.Id, user.Id, description);
            //update
            await _dObject.PostRatingService.AddOrUpdatePostRating(ratingStarsUpdated, post.Id, user.Id, descriptionUpdated);

            var ratingObj = await _dObject.Context.PostRatings.Where(p => p.PostId == post.Id && p.UserId == user.Id).FirstAsync();
            Assert.Equal(ratingStarsUpdated, ratingObj.RatingStars);
            Assert.Equal(descriptionUpdated, ratingObj.Description);
        }
        [Fact]
        public async Task AddOrUpdatePostRating_UnpublishedPost_ShouldThrowInvalidOperationException()
        {
            var user = await _dObject.CreateUser();
            var post = await _dObject.CreatePost(user.Id, false);
            var ratingStars = 3;
            var description = "some description";

            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await _dObject.PostRatingService.AddOrUpdatePostRating(ratingStars, post.Id, user.Id, description));
        }
    }
}
