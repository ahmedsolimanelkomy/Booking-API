using Booking_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelDTOS
{
    public class FilteredHotelDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Rating { get; set; }

        public ICollection<HotelPhoto>? Photos { get; set; } = new HashSet<HotelPhoto>();
        public string? Description { get; set; }
        public string? CityName { get; set; }

        public decimal? Price { get; set; }



    }
}
