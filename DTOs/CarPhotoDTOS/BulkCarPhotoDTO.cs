using Booking_API.Models;

namespace Booking_API.DTOs.CarPhotoDTOS
{
    public class BulkCarPhotoDTO
    {
        public List<IFormFile> Photos { get; set; }
        public CarPhotoCat Category { get; set; }
    }
}
