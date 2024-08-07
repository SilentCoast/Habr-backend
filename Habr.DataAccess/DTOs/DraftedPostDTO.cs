namespace Habr.DataAccess.DTOs
{
    public class DraftedPostDto
    {
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
