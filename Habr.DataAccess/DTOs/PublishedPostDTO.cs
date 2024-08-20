﻿namespace Habr.DataAccess.DTOs
{
    public class PublishedPostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
