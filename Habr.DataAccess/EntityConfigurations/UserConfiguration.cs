﻿using Habr.DataAccess.Constraints;
using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habr.DataAccess.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(ConstraintValue.UserNameMaxLength);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(ConstraintValue.UserEmailMaxLength);

            builder.HasIndex(p => p.Email)
                .IsUnique();

            builder.Property(p => p.PasswordHash)
                .IsRequired()
                .HasMaxLength(ConstraintValue.UserPasswordHashMaxLength);

            builder.Property(p => p.Salt)
                .IsRequired()
                .HasMaxLength(ConstraintValue.UserSaltMaxLength);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.HasMany(p => p.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Comments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(p => p.RefreshTokens)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired();
        }
    }
}
