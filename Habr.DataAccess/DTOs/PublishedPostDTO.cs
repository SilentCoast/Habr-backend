namespace Habr.DataAccess.DTOs
{
    public class PublishedPostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string AuthorEmail { get; set; }
        //TODO: rename to PublishedAt in the next api version
        public DateTime PublishDate { get; set; }
    }
}
