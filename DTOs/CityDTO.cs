using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs
{
    public class CityDTO
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int CountryId { get; set; }
    }
}
