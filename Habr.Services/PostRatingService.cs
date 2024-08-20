using Habr.DataAccess;
using Habr.DataAccess.Constraints;
using Habr.DataAccess.Entities;
using Habr.Services.Interfaces;
using Habr.Services.Resources;
using Microsoft.EntityFrameworkCore;

namespace Habr.Services
{
    public class PostRatingService(DataContext context) : IPostRatingService
    {
        private readonly DataContext _context = context;

        public async Task AddOrUpdatePostRating(int ratingStars, int postId, int userId,
            CancellationToken cancellationToken = default)
        {
            CheckRatingConstraints(ratingStars);

            var post = await _context.Posts.SingleOrDefaultAsync(p => p.Id == postId, cancellationToken)
                ?? throw new ArgumentException(ExceptionMessage.PostNotFound);

            if (post.IsPublished == false)
            {
                throw new InvalidOperationException(ExceptionMessage.CannotRateUnpublishedPost);
            }

            var rating = await _context.PostRatings.SingleOrDefaultAsync(p => p.PostId == postId && p.UserId == userId, cancellationToken);
            if (rating != null)
            {
                rating.RatingStars = ratingStars;
                _context.Update(rating);
            }
            else
            {
                rating = new PostRating()
                {
                    RatingStars = ratingStars,
                    RatedAt = DateTime.UtcNow,
                    PostId = postId,
                    UserId = userId
                };
                _context.Add(rating);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAveragePostRatings(CancellationToken cancellationToken = default)
        {
            await _context.Posts
                .Where(p => p.PostRatings.Count != 0)
                .ExecuteUpdateAsync(p => p.SetProperty(
                    post => post.AverageRating,
                    post => _context.PostRatings
                        .Where(r => r.PostId == post.Id)
                        .Average(r => r.RatingStars)
                ), cancellationToken);
        }

        private static void CheckRatingConstraints(int rating)
        {
            if (rating < ConstraintValue.PostRatingStarsMin || rating > ConstraintValue.PostRatingStarsMax)
            {
                throw new ArgumentOutOfRangeException(string.Format(ExceptionMessageGeneric.RatingOutOfBounds,
                    ConstraintValue.PostRatingStarsMin, ConstraintValue.PostRatingStarsMax));
            }
        }
    }
}
