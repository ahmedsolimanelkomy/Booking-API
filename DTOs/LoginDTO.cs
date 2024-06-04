using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        

    }
}
