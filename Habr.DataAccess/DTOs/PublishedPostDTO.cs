namespace Habr.DataAccess.DTOs
{
    public class PublishedPostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string AuthorEmail { get; set; }
        /// <summary>
        /// renamed to PublishedAt in <see cref="PublishedPostV2Dto.PublishedAt"/>
        /// </summary>
        public DateTime PublishDate { get; set; }
    }
}
