using Booking_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.CarDTOS
{
    public class FilteredCarDTO
    {
        public int Id { get; set; }

        public int CarAgencyId { get; set; }

        public decimal? RentPrice { get; set; }

        public bool? AvailabilityStatus { get; set; }

        [MaxLength(20)]
        public string? PlateNumber { get; set; }

        public bool? InsuranceIncluded { get; set; }

        public GearType? GearType { get; set; }

        public GasType? GasType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "NumberOfSeats must be at least 1")]
        public int? NumberOfSeats { get; set; }

        public string? Description { get; set; }

        public int? ModelOfYear { get; set; }
        public string? Brand { get; set; }

        public string? AgencyName { get; set; }

        public CarType? CarType { get; set; }

        public ICollection<CarPhoto>? CarPhotos { get; set; } = new HashSet<CarPhoto>();
    }
}
