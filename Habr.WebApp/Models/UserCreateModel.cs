using Habr.DataAccess.Constraints;
using System.ComponentModel.DataAnnotations;

namespace Habr.WebApp.Models
{
    public class UserCreateModel
    {
        [Required]
        [EmailAddress]
        [MaxLength(ConstraintValue.UserEmailMaxLength)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Name { get; set; }
    }
}
