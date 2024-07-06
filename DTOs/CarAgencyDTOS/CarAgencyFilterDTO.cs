using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.CarAgencyDTOS
{
    public class CarAgencyFilterDTO
    {

        public string? Name { get; set; }

        public string? Address { get; set; }

        public int? CityId { get; set; }

    }
}
