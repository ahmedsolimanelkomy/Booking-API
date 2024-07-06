using Booking_API.Models;
using Booking_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.CarRental
{
    public class UserCarRentalFilterDTO
    {
        [Required]
        public int UserId { get; set; }

        public string? PickUpCity { get; set; }

        public DateTime? PickUpDate { get; set; }

        public DateTime? DropOffDate { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public GearType? GearType { get; set; }
        public int? ModelOfYear { get; set; }
        public string? Brand { get; set; }
        public bool? InsuranceIncluded { get; set; }
        public int? NumberOfSeats { get; set; }
        public string? AgencyName { get; set; }
        public BookingStatus? Status { get; set; }
    }
}
