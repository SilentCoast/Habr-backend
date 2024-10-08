﻿namespace Habr.DataAccess.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public UserInCommentDto User { get; set; }
        public bool IsDeleted { get; set; }
        public List<CommentDto> Replies { get; set; }
    }
}
