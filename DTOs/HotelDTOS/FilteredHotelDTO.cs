using Booking_API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Booking_API.DTOs.RoomDTOS;
using Booking_API.DTOs.FeatureDTOS;

namespace Booking_API.DTOs.HotelDTOS
{
    public class FilteredHotelDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? Rating { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Description { get; set; }
        public string? CityName { get; set; }
        public decimal? Price { get; set; }
        public ICollection<HotelPhoto>? Photos { get; set; } = new HashSet<HotelPhoto>();
        public ICollection<FilteredRoomHotelDTO>? Rooms { get; set; } = new HashSet<FilteredRoomHotelDTO>();
        public ICollection<FeatureDTO>? Features { get; set; } = new HashSet<FeatureDTO>();



    }
}
