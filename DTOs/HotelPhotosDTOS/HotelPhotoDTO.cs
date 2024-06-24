using Booking_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelPhotosDTOS
{
    public class HotelPhotoDTO
    {
        public int Id { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

        [Required]
        public PhotoCategory Category { get; set; }
    }
}
