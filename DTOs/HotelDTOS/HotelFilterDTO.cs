using Booking_API.Models;

namespace Booking_API.DTOs.HotelDTOS
{
    public class HotelFilterDTO
    {
        public int? CityId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? RoomTypeId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public View? RoomView { get; set; }
        public List<int>? FeatureIds { get; set; }
        public int? RoomCapacity { get; set; }
    }
}
