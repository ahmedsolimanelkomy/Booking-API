using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class Room
    {
        public int ID { get; set; }
        public string RoomType { get; set; }
        public bool AvalabilityStatus { get; set; }
        public int Capacity { get; set; }
        public string View { get; set; }
        public bool IsBooked { get; set; }
        [ForeignKey("Hotel")]
        public int HotelId { get; set; }

        [ForeignKey("RoomType")]
        public int?  RoomTypeID { get; set; }

        //[ForeignKey"Booking"]
        //public virtual ICollection<Booking> Bookings { get; set; } = new HashSet<Bookings>;

    }
}
