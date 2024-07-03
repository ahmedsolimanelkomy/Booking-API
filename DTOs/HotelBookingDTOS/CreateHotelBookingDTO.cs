using Booking_API.Models;
using Booking_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelBookingDTOS
{
    public class CreateHotelBookingDTO
    {
        public int? Id { get; set; }
        [Required]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total Price must be a positive number")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

        public string? Notes { get; set; }

        [CheckInDate]
        public DateTime CheckInDate { get; set; }

        [CheckOutDate("CheckInDate", ErrorMessage = "Check-out date must be after the check-in date.")]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public int HotelId { get; set; }
    }
}
