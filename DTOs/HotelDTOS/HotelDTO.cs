using Booking_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelDTOS
{
    public class HotelDTO
    {
        public int? Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        //[Range(0, 5)]
        //public int? Rating { get; set; }


        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Url]
        public string? WebSiteURL { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public int? CityId { get; set; }



    }
}
