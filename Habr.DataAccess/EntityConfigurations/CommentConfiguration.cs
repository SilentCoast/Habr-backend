using Habr.DataAccess.Constraints;
using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habr.DataAccess.EntityConfigurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(ConstraintValue.CommentTextMaxLength);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.IsDeleted)
                .HasDefaultValue(false);

            builder.HasMany(p => p.Replies)
                .WithOne(p => p.ParentComment)
                .HasForeignKey(p => p.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
