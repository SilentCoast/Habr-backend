using Habr.DataAccess.Constraints;
using Habr.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Habr.DataAccess.EntityConfigurations
{
    public class PostRatingConfiguration : IEntityTypeConfiguration<PostRating>
    {
        public void Configure(EntityTypeBuilder<PostRating> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.RatingStars)
            .IsRequired();

            builder.ToTable(t =>
                t.HasCheckConstraint("CK_Rating_RatingStars_Range", $"{nameof(PostRating.RatingStars)} " +
                $"BETWEEN {ConstraintValue.PostRatingStarsMin} AND {ConstraintValue.PostRatingStarsMax}"));

            builder.Property(p => p.RatedAt)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(ConstraintValue.PostRatingDescriptionMaxLength);

            builder.HasOne(p => p.Post)
                .WithMany(p => p.PostRatings)
                .HasForeignKey(p => p.PostId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.User)
                .WithMany(p => p.PostRatings)
                .HasForeignKey(p => p.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(p => new { p.PostId, p.UserId })
               .IsUnique()
               .HasDatabaseName("IX_Rating_Post_User_Unique");
        }
    }
}
