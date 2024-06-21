using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.AdminDTOS
{
    public class CreateAdminDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? UserName { get; set; }

        public string? Gender { get; set; }
    }
}
