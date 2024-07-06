using Booking_API.Models;
using Booking_API.Validations;

namespace Booking_API.DTOs.CarRental
{
    public class CarRentalDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int CarId { get; set; }
        public int AgencyId { get; set; }
        public DateTime? RentDate { get; set; }
        public BookingStatus? Status { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Notes { get; set; }
        public double? PickUpLocation { get; set; }
        public double? DropOffLocation { get; set; }

        [CheckInDate]
        public DateTime PickUpDate { get; set; }

        [CheckOutDate("PickUpDate", ErrorMessage = "Check-out date must be after the check-in date.")]
        public DateTime DropOffDate { get; set; }

    }
}
