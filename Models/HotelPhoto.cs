using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.Models
{
    public class HotelPhoto
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? PhotoUrl { get; set; }

        //Foreign Keys

        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }

        //Navigation Properties
        public Hotel? Hotel { get; set; }
    }
}
