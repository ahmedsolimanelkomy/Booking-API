using Booking_API.Models;

namespace Booking_API.DTOs
{
    public class CarDTO
    {
        public int Id { get; set; }

        //[Range(1886, 2100, ErrorMessage = "Year must be between 1886 and 2100")]
        public int? ModelOfYear { get; set; }

        //   [MaxLength(50)]
        public string? Brand { get; set; }

        // [Range(0, double.MaxValue, ErrorMessage = "RentPrice must be a positive value")]
        public decimal? RentPrice { get; set; }

        public bool? AvailabilityStatus { get; set; }

        // [MaxLength(20)]
        public string? PlateNumber { get; set; }

        public bool? InsuranceIncluded { get; set; }


        public GearType? GearType { get; set; }

        // [Range(1, int.MaxValue, ErrorMessage = "NumberOfSeats must be at least 1")]
        public int? NumberOfSeats { get; set; }

        //public int? AgencyId { get; set; }
        //public int? CarTypeId { get; set; }

        //// Optional: include agency and car type names
        //public string? AgencyName { get; set; }
        //public string? CarTypeName { get; set; }
    }
}
