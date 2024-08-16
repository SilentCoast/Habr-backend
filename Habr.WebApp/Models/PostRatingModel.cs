using Habr.DataAccess.Constraints;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Models
{
    public class PostRatingModel
    {
        [Range(1, 5)]
        public int RatingStars { get; set; }
        [MaxLength(ConstraintValue.PostRatingDescriptionMaxLength)]
        public string? Description { get; set; }
    }
}
