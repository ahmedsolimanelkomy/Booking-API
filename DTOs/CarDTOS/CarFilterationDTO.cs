using Booking_API.Validations;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Booking_API.DTOs.CarDTOS
{
    public class CarFilterationDTO
    {
        public int? CityId { get; set; }

        public DateTime PickUpDate { get; set; }

        public DateTime DropOffDate { get; set; }

        [Range(0, 5)]
        public int? Rating { get; set; }

        public string? Description { get; set; }

        public int? RoomTypeId { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public List<int>? FeatureIds { get; set; }

        public int? RoomCapacity { get; set; }
    }
}
