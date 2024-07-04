using Booking_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelBookingDTOS
{
    public class UserBookingFilterDTO
    {
        [Required]
        public int UserId { get; set; }
        public string? HotelName { get; set; }
        public string? UserName { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public int? RoomNumber { get; set; }
        public BookingStatus? Status { get; set; }
        public decimal? Price { get; set; }
    }
}
