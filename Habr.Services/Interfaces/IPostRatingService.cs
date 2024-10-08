﻿namespace Habr.Services.Interfaces
{
    public interface IPostRatingService
    {
        Task AddOrUpdatePostRating(int ratingStars, int postId, int userId,
            CancellationToken cancellationToken = default);
        Task UpdateAveragePostRatings(CancellationToken cancellationToken = default);
    }
}
