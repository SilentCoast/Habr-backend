namespace Habr.DataAccess.DTOs
{
    public class PostViewDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string AuthorEmail { get; set; }
        //TODO: rename to PublishedAt in the next api version
        public DateTime? PublishDate { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
