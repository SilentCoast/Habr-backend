namespace Habr.DataAccess.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public UserInCommentDTO User { get; set; }
        public bool IsDeleted { get; set; }
        public List<CommentDTO> Replies { get; set; }
    }
}
