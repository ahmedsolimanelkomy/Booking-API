using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Currency)]
        public decimal PricePerNight { get; set; }

        //Foreign Keys

        //Navigation Properties

        [ForeignKey("Room")]
        public ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
    }
}
