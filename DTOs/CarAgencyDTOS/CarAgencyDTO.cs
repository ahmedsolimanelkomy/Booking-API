namespace Booking_API.DTOs.CarAgencyDTOS
{
    public class CarAgencyDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int? CityId {  get; set; }
        public IFormFile? AgencyPhoto { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteURL { get; set; }
        public string? Email { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
    }
}
