using Booking_API.Models;

namespace Booking_API.DTOs.CarPhotoDTOS
{
    public class UpdateCarPhotoDTO
    {
        public string Id { get; set; }
        public IFormFile Photo { get; set; }
        public CarPhotoCat Category { get; set; }
    }
}
