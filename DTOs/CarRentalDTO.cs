using Booking_API.Models;

namespace Booking_API.DTOs
{
    public class CarRentalDTO
    {
        public int Id { get; set; }

        // [Required(ErrorMessage = "RentDate is required")]
        public DateTime? RentDate { get; set; }

        // [Required(ErrorMessage = "Status is required")]
        public BookingStatus? Status { get; set; }

        // [Required(ErrorMessage = "TotalPrice is required")]
        //  [Range(0, double.MaxValue, ErrorMessage = "TotalPrice must be a positive value")]
        public decimal? TotalPrice { get; set; }

        public string? Notes { get; set; }

        //[Required(ErrorMessage = "PickUpLocation is required")]
        public double? PickUpLocation { get; set; }

        //   [Required(ErrorMessage = "DropOffLocation is required")]
        public double? DropOffLocation { get; set; }

        //   [Required(ErrorMessage = "PickUpDate is required")]
        public DateTime? PickUpDate { get; set; }

        //  [Required(ErrorMessage = "DropOffDate is required")]
        public DateTime? DropOffDate { get; set; }

    }
}
