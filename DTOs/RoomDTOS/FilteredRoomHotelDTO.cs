using Booking_API.Models;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.RoomDTOS
{
    public class FilteredRoomHotelDTO
    {
        public int Id { get; set; }
        public bool AvailabilityStatus { get; set; }
        public int Capacity { get; set; }
        public View? View { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int? HotelBookingId { get; set; }
        public string? TypeName { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
