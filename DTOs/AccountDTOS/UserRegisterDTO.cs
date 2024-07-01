using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.AccountDTOS
{
    public class UserRegisterDTO
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }

        [EmailAddress, Required]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
