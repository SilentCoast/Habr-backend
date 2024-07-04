using Habr.DataAccess.Constraints;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Models
{
    public class PostUpdateModel
    {
        [MaxLength(ConstraintValue.PostTitleMaxLength)]
        public string? NewTitle { get; set; }

        [MaxLength(ConstraintValue.PostTextMaxLength)]
        public string? NewText { get; set; }
    }
}
