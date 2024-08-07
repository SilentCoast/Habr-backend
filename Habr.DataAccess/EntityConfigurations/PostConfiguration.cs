using Habr.DataAccess.Constraints;
using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habr.DataAccess.EntityConfigurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(ConstraintValue.PostTitleMaxLength);

            builder.Property(p => p.Text)
                .IsRequired()
                .HasMaxLength(ConstraintValue.PostTextMaxLength);

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.HasMany(p => p.Comments)
                .WithOne(p => p.Post)
                .HasForeignKey(p => p.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
