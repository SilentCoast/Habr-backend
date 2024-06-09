namespace Habr.DataAccess.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        /// <summary>
        /// Nullabla so when User is deleted, comments are still visible, but with remark 'user deleted'
        /// </summary>
        public int? UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
