using Booking_API.Models;

namespace Booking_API.DTOs.HotelDTOS
{
    public class WishlistHotelDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CityName { get; set; }
        public decimal? Price { get; set; }
        public ICollection<HotelPhoto>? Photos { get; set; } = new HashSet<HotelPhoto>();
    }
}
