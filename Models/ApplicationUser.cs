﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        [StringLength(100)]
        public string? FirstName { get; set; }
        [StringLength(100)]
        public string? LastName { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }
        public string? PhotoUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        // Foreign keys
        [ForeignKey("WishList")]
        public int? WishListId { get; set; }
        [ForeignKey("Passport")]
        public int? PassportId { get; set; }
        [ForeignKey("City")]
        public int? CityId { get; set; }

        // Navigation Properties
        public HotelWishList? WishList { get; set; }
        public Passport? Passport { get; set; }
        public City? City { get; set; }

        public ICollection<HotelBooking>? Bookings { get; set; }
<<<<<<< HEAD
        public ICollection<HotelReview>? Reviews { get; set; }

=======

        public CarRentalInvoice? CarRentalInvoice { get; set; }
        public HotelBookingInvoice? HotelBookingInvoice { get; set; }
>>>>>>> d8b71818a746eb3242ae0f867c802e98f1cb990d
    }
}
