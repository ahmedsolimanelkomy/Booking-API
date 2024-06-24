using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Booking_API.Validations;

namespace Booking_API.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Range(0, 5)]
        public int? Rating { get; set; }
        

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Url]
        public string? WebSiteURL { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        //Foreign Keys

        [ForeignKey("City")]
        public int? CityId { get; set; }

        //Navigation Properties
        public City? City { get; set; }    
        public ICollection<Room> Rooms { get; set; } = new HashSet<Room>();
        public ICollection<Feature> Features { get; set; } = new HashSet<Feature>();
        public ICollection<HotelPhoto> Photos { get; set; } = new HashSet<HotelPhoto>();
        public ICollection<HotelBooking> HotelBookings { get; set; } = new HashSet<HotelBooking>();
        
    }
}
