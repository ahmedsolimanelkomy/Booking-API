using Booking_API.Models;

namespace Booking_API.DTOs.HotelBookingDTOS
{
    public class HotelBookingFilterDTO
    {
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? RoomNumber { get; set; }
        public string? HotelName { get; set; }
        public string? UserName { get; set; }
        public BookingStatus? Status { get; set; }
        public decimal? Price { get; set; }
    }
}
