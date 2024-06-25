using Booking_API.Models;

namespace Booking_API.DTOs.RoomDTOs
{
    public class FilteredRoomDTO
    {
        public int Id { get; set; }
        public bool AvailabilityStatus { get; set; }
        public int Capacity { get; set; }
        public View? View { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int? HotelBookingId { get; set; }
    }
}
