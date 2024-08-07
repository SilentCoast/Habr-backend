namespace Habr.DataAccess.DTOs
{
    public class PostViewDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime? PublishDate { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
