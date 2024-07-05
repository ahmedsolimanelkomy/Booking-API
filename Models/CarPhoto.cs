using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Booking_API.Models
{
    public enum CarPhotoCat
    {
        FrontView = 0,
        BackView = 1,
        Interior = 2
    }
    public class CarPhoto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PhotoUrl { get; set; }

        public CarPhotoCat Category { get; set; } = CarPhotoCat.FrontView;

        //Foreign Keys

        [ForeignKey("Car")]
        public int? CarId { get; set; }

        //Navigation Properties
        [JsonIgnore]
        public Car? Car { get; set; }
    }
}
