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

        [MaxLength(100)]
        public string? View { get; set; }

        public bool IsBooked { get; set; }

        // Foreign Keys
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        [ForeignKey("RoomType")]
        public int? RoomTypeId { get; set; }

        // Navigation Properties
        public Hotel? Hotel { get; set; }

        public RoomType? RoomType { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();

    }
}
