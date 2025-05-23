using System.ComponentModel.DataAnnotations;

namespace WorldTracker.Web.DTOs
{
    public class AuthDataDto
    {
        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
