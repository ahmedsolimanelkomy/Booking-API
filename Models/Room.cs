using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public bool AvailabilityStatus { get; set; }

        public int Capacity { get; set; }

        public string? View { get; set; }

        // Foreign Keys
        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }

        [ForeignKey("RoomType")]
        public int? RoomTypeId { get; set; }

        // Navigation Properties
        public Hotel? Hotel { get; set; }

        public RoomType? RoomType { get; set; }

        

    }
}
