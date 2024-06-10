using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class AdminDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Gender { get; set; }

    }
}
