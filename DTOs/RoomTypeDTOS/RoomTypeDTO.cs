using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.NewFolder
{
    public class RoomTypeDTO
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal PricePerNight { get; set; }
    }
}
