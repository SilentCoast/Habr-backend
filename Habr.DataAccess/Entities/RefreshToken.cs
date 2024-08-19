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
        //TODO: remove JsonIgnore and configure use of RefreshTokenDto instead
        [JsonIgnore]
        public User User { get; set; }
    }
}
