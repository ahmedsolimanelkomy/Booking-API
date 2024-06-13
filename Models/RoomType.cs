using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Booking_API.Models
{
    public class RoomType
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal PricePerNight { get; set; }

        //Foreign Keys

        //Navigation Properties
        [JsonIgnore]
        public ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
    }
}
