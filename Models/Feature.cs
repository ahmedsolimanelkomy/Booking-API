using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class Feature
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        //Foreign Keys

        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }

        //Navigation Properties
        public Hotel? Hotel { get; set; }
    }
}
