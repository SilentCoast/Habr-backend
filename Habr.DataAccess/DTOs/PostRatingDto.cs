namespace Habr.DataAccess.DTOs
{
    public class PostRatingDto
    {
        public int Id { get; set; }
        public int RatingStars { get; set; }
        public DateTime RatedAt { get; set; }
    }
}
