using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class RegisterDTO
    {
      
            [Required]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public string FullName { get; set; }
            public string Gender { get; set; }
            public string Address { get; set; }
            public string PhotoUrl { get; set; }

            [DataType(DataType.Date)]
            public DateOnly BirthDate { get; set; }
            




    }
}
