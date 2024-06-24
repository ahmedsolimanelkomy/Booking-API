using Booking_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelPhotosDTOS
{
    public class GetHotelPhotoDTO
    {
        public int Id { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        [Required]
        public PhotoCategory Category { get; set; }
    }
}
