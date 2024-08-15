namespace Habr.DataAccess.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        //TODO: rename to ModifiedAt in the next api version
        public DateTime? ModifiedDate { get; set; }
        public UserInCommentDto User { get; set; }
        public bool IsDeleted { get; set; }
        public List<CommentDto> Replies { get; set; }
    }
}
