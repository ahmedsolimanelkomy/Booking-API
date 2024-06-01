using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PriceRange { get; set; }

        public string Features { get; set; }

        public int Rating { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string WebSiteURL { get; set; }

        //[ForeignKey"City"]
        //public int? CityID { get; set; }

        //[ForeignKey"Room"]
        //public ICollection<Room> Rooms { get; set; } = new HashSet<Room>;

        //public Hotel()
        //{
        //    Rooms = new HashSet<Room>;
        //}
        [ForeignKey("wishList")]
        public virtual ICollection<WishList> WishList { get; set; } = new HashSet<WishList>();
    }
}
