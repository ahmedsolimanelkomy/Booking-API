using Booking_API.Models;

namespace Booking_API.DTOs.CarDTOS
{
    public class CarDTO
    {
        public int Id {  get; set; }
        public int AgencyId { get; set; }
        public int? ModelOfYear { get; set; }
        public string? Brand { get; set; }
        public decimal? RentPrice { get; set; }
        public bool? AvailabilityStatus { get; set; }
        public string? PlateNumber { get; set; }
        public string? Description { get; set; }
        public bool? InsuranceIncluded { get; set; }
        public GearType? GearType { get; set; }
        public GasType? GasType { get; set; }
        public int? NumberOfSeats { get; set; }
        public ICollection<CarPhoto>? CarPhotos { get; set; } = new HashSet<CarPhoto>();
    }
}
