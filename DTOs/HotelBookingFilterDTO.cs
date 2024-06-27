using Booking_API.Models;

namespace Booking_API.DTOs.HotelDTOS
{
    public class HotelBookingFilterDTO
    {
        public int? HotelId { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? RoomNumber { get; set; }
        public int? UserId { get; set; }
        public int? RoomId { get; set; }
        public BookingStatus? Status { get; set; }
    }
}
