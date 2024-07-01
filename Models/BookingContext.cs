﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Models
{
    public class BookingContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<HotelBooking> HotelBooking { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarAgency> CarAgencies { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<HotelReview> Reviews { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<HotelWishList> WishLists { get; set; }
        public DbSet<HotelBookingInvoice> hotelBookingInvoices { get; set; }
        public DbSet<CarRental> carRentals { get; set; }

        public DbSet<CarRentalInvoice> carRentalInvoices { get; set; }
        public DbSet<CarAgencyReview> carAgencyReviews { get; set; }
        public BookingContext(DbContextOptions<BookingContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
