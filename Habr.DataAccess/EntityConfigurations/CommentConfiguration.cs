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
                .HasMaxLength(10000);
        }
    }
}
