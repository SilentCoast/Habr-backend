using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habr.DataAccess.EntityConfigurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Token)
               .IsRequired()
               .HasMaxLength(50);

            builder.Property(p => p.ExpiresAt)
               .IsRequired();

            builder.Property(p => p.CreatedAt)
               .IsRequired();
        }
    }
}
