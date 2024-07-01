using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.AccountDTOS
{
    public class LoginDTO
    {
        [Required]
        public string? UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


    }
}
