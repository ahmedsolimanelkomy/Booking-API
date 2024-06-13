using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class UserRegisterDTO
    {

        public string? UserName { get; set; }
        public string? FirstName { get; set; }
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
