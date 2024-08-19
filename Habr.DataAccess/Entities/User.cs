namespace Habr.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        public int TokenVersion { get; set; } = 1;
        public ICollection<PostRating> PostRatings { get; set; }
    }
}
