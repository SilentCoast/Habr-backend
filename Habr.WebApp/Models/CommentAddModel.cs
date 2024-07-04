using Habr.DataAccess.Constraints;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Models
{
    public class CommentAddModel
    {
        [Required]
        [MaxLength(ConstraintValue.CommentTextMaxLength)]
        public string Text { get; set; }
        [Required]
        public int PostId { get; set; }
    }
}
