using Booking_API.Models;

namespace Booking_API.DTOs.HotelDTOS
{
    public class FilteredBookingDTO
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string Notes { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int HotelId { get; set; }

        // You can include additional properties as needed
    }
}
