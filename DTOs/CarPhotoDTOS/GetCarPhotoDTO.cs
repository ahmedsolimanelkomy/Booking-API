using Booking_API.Models;

namespace Booking_API.DTOs.CarPhotoDTOS
{
    public class GetCarPhotoDTO
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public CarPhotoCat Category { get; set; }
    }
}
