using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public int PricePerHour { get; set; }

        [ForeignKey("Room")]
        public virtual ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
    }
}
