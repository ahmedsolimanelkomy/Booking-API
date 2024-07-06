using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.CarAgencyDTOS
{
    public class CarAgencyViewDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? CityId { get; set; }
        [Url]
        public string? AgencyPhotoURL { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteURL { get; set; }
        public string? Email { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}
