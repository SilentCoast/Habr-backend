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
                .HasMaxLength(100);

            builder.Property(p => p.Email)
                .HasMaxLength(100);

            builder.HasMany(p => p.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Comments)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                //Should probably be .SetNull but SqlServer does not allows multiple cascade pathways,
                //and now after User is deleted this comments will have non-existent UserIds
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
