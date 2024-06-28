using Booking_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelPhotosDTOS
{
    public class BulkHotelPhotoDTO
    {

        [Required]
        public IFormFile[] Photos { get; set; }

        [Required]
        public PhotoCategory Category { get; set; }
    }
}
