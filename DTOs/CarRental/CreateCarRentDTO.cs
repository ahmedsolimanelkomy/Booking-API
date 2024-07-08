using Booking_API.Models;
using Booking_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.CarRental
{
    public class CreateCarRentDTO
    {
        public int? Id { get; set; }
        [Required]
        public DateTime RentDate { get; set; }

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total Price must be a positive number")]
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

        public string? Notes { get; set; }

        [Required]
        public double PickUpLocation { get; set; }

        [Required]
        public double DropOffLocation { get; set; }

        [CheckInDate]
        public DateTime PickUpDate { get; set; }

        [CheckOutDate("PickUpDate", ErrorMessage = "Check-out date must be after the check-in date.")]
        public DateTime DropOffDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public int CarAgencyId { get; set; }
    }
}
