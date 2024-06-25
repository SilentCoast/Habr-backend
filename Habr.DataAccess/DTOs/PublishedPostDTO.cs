namespace Habr.DataAccess.DTOs
{
    public class PublishedPostDTO
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
