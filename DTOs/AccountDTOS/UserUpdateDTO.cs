namespace Booking_API.DTOs.AccountDTOS
{
    public class UserUpdateDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public IFormFile? Photo { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
    }
}
