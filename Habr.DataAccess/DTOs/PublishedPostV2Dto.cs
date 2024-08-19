namespace Habr.DataAccess.DTOs
{
    public class PublishedPostV2Dto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public DateTime PublishedAt { get; set; }
        public PostAuthorDto Author { get; set; }
        public double? AvgRating { get; set; }
    }
}
