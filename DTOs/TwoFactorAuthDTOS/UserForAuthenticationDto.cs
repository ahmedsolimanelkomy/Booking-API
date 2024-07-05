using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.TwoFactorAuthDTOS
{
    public class UserForAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
