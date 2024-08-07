﻿namespace Habr.Services.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, string salt);
        string GenerateSalt();
    }
}
