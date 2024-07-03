namespace Booking_API.DTOs
{
    public class CarAgencyDTO
    {
        public int Id { get; set; }

        //  [MaxLength(100)]
        public string? Name { get; set; }

        //  [MaxLength(200)]
        public string? Address { get; set; }

        //  [Url]
        public string? LogoURL { get; set; }

        //  [Phone]
        public string? PhoneNumber { get; set; }

        // [Url]
        public string? WebsiteURL { get; set; }

        // [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }



    }
}
