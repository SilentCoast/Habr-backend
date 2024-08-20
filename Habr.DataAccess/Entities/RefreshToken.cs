﻿using System.Text.Json.Serialization;

namespace Habr.DataAccess.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
