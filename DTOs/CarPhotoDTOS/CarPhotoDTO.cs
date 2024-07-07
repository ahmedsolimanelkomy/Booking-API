using Booking_API.Models;

namespace Booking_API.DTOs.CarPhotoDTOS
{
    public class CarPhotoDTO
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public CarPhotoCat Category { get; set; }
    }
}
