using Habr.DataAccess.Entities;

namespace Habr.WebApp.Models
{
    public class PostDTO
    {
        public string Title {  get; set; }
        public string Text {  get; set; }
        public string AuthorEmail {  get; set; }
        public DateTime? PublishDate { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
