using Booking_API.Models;

namespace Booking_API.DTOs.CarPhotoDTOS
{
    public class CreateCarPhotoDTO
    {
        public IFormFile Photo { get; set; }
        public CarPhotoCat Category { get; set; }
    }
}
