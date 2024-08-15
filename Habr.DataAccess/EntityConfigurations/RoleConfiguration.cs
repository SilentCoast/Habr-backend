using Habr.DataAccess.Entities;
using Habr.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habr.DataAccess.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.RoleType)
                .IsRequired()
                .HasConversion
                (
                    p => p.ToString(),
                    p => (RoleType)Enum.Parse(typeof(RoleType), p)
                );

            builder.HasMany(p => p.Users)
                .WithOne(p => p.Role)
                .HasForeignKey(p => p.RoleId)
                .IsRequired();

            builder.HasData
                (
                new Role { Id = 1, RoleType = RoleType.User, Name = "User" },
                new Role { Id = 2, RoleType = RoleType.Admin, Name = "Admin" }
                );
        }
    }
}
