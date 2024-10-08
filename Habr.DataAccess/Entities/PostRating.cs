﻿namespace Habr.DataAccess.Entities
{
    public class PostRating
    {
        public int Id { get; set; }
        public int RatingStars { get; set; }
        public DateTime RatedAt { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
