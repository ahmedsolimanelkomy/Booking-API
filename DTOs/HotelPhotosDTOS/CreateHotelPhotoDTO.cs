using Booking_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelPhotosDTOS
{
    public class CreateHotelPhotoDTO
    {
        [Required]
        public IFormFile Photo { get; set; }

        [Required]
        public PhotoCategory Category { get; set; }
    }
}
