using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Booking_API.Models
{
    public enum PhotoCategory
    {
        FrontView = 1,
        BackView = 2,
        Rooms = 3,
        Garden = 4,
        Pools = 5,
        Reception = 6
    }
    public class HotelPhoto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        public PhotoCategory Category { get; set; } = PhotoCategory.FrontView;

        //Foreign Keys

        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public Hotel? Hotel { get; set; }
    }
}
