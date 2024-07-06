using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.AccountDTOS
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime? BirthDate { get; set; }

    }
}
