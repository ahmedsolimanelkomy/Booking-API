using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public enum View
    {
        view1,
        view2,
        view3
    }
    public class Room
    {   
        [Key]
        public int Id { get; set; }
        public bool AvailabilityStatus { get; set; }
        public int Capacity { get; set; }
        public View? View { get; set; }
        public bool IsBooked { get; set; }

        // Foreign Keys
        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }

        [ForeignKey("RoomType")]
        public int? RoomTypeId { get; set; }

        // Navigation Properties
        public Hotel? Hotel { get; set; }

        public RoomType? RoomType { get; set; }

        

    }
}
