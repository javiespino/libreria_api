using System.ComponentModel.DataAnnotations;

namespace javier_api.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(user|admin)$")]
        public string Role { get; set; } = "user";
    }
}