using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.AdminDTOS
{
    public class AdminDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Current password is required")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        public string? UserName { get; set; }

        public string? Gender { get; set; }
    }
}

