﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public enum View
    {
        Garden = 1,
        Sea = 2,
        City = 3,
        Mountain = 4,
        Pool = 5
    }
    public class Room
    {   
        [Key]
        public int Id { get; set; }
        public bool AvailabilityStatus { get; set; }
        public int Capacity { get; set; }
        public View? View { get; set; }
        public bool IsBooked { get; set; }
        public int RoomNumber { get; set; }

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
