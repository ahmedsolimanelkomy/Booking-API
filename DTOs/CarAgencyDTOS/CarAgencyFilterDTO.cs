using Booking_API.Models;
using Booking_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.CarAgencyDTOS
{
    public class CarAgencyFilterDTO
    {
        [CheckInDate]
        public DateTime PickUpDate { get; set; }

        [CheckOutDate("PickUpDate", ErrorMessage = "DropOffDate must be after the PickUpDate.")]
        public DateTime DropOffDate { get; set; }
        public int? CityId { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public GearType? GearType { get; set; }
        public int? ModelOfYear { get; set; }
        public string? Brand { get; set; }
        public bool? InsuranceIncluded { get; set; }
        public int? NumberOfSeats { get; set; }
        public int? AgencyId { get; set; }
    }
}
