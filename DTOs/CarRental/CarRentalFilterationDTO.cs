using Booking_API.Models;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Booking_API.DTOs.CarRental
{
    public class CarRentalFilterationDTO
    {
        //Main Filteration 
        public string? PickUpCity { get; set; }

        public DateTime? PickUpDate { get; set; }

        public DateTime? DropOffDate { get; set; }

        //Second Filteration

        public decimal? Price { get; set;}
        public GearType? GearType { get; set;}

        [Range(1886, 2100, ErrorMessage = "Year must be between 1886 and 2100")]
        public int? ModelOfYear { get; set; }

        [MaxLength(50)]
        public string? Brand { get; set; }
        public bool? InsuranceIncluded { get; set; }
        public int? NumberOfSeats { get; set; }
        public string? AgencyName { get; set; }

    }
}
