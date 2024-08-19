namespace Habr.DataAccess.Entities
{
    //TODO: currently we don't have a way to track when or by who post was drafted
    public class Post
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        //TODO: add DateTime? ModifiedBy
        public bool IsPublished { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; } = [];
        public double? AverageRating { get; set; }
        public ICollection<PostRating> PostRatings { get; set; } = [];
    }
}
