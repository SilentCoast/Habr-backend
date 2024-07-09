using Habr.DataAccess.Constraints;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Models
{
    public class PostCreateModel
    {
        [Required]
        [MaxLength(ConstraintValue.PostTitleMaxLength)]
        public string Title { get; set; }
        [Required]
        [MaxLength(ConstraintValue.PostTextMaxLength)]
        public string Text { get; set; }
        public bool IsPublishedNow { get; set; }
    }
}
