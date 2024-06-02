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

        public string? PriceRange { get; set; }

        public string? Features { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }
        [CheckInDate]
        public DateTime CheckInDate { get; set; }
        [CheckOutDate("CheckOutDate", ErrorMessage = "Check-out date must be later than check-in date.")]
        public DateTime CheckOutDate { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Url]
        public string? WebSiteURL { get; set; }

        //Foreign Keys

        [ForeignKey("City")]
        public int? CityId { get; set; }

        //Navigation Properties
        public City? City { get; set; }    

        [ForeignKey("Room")]
        public ICollection<Room> Rooms { get; set; } = new HashSet<Room>();

        [ForeignKey("WishList")]
        public virtual ICollection<WishList> WishLists { get; set; } = new HashSet<WishList>();
    }
}
