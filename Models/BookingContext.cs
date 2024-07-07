using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Booking_API.Models
{
    public class BookingContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<HotelBooking> HotelBooking { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarAgency> CarAgencies { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<CarReview> CarAgencyReviews { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<HotelReview> Reviews { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<HotelWishList> WishLists { get; set; }
        public DbSet<CarRentalInvoice> CarRentalInvoices { get; set; }
        public DbSet<HotelBookingInvoice> HotelBookingInvoices { get; set; }
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

            // Seed data for Countries
            builder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "USA" },
                new Country { Id = 2, Name = "Canada" },
                new Country { Id = 3, Name = "Germany" }
            );

            // Seed data for Cities
            builder.Entity<City>().HasData(
                new City { Id = 1, Name = "New York", Code = "NY", CountryId = 1 },
                new City { Id = 2, Name = "Los Angeles", Code = "LA", CountryId = 1 },
                new City { Id = 3, Name = "Toronto", Code = "TO", CountryId = 2 },
                new City { Id = 4, Name = "Vancouver", Code = "VA", CountryId = 2 },
                new City { Id = 5, Name = "Berlin", Code = "BE", CountryId = 3 },
                new City { Id = 6, Name = "Munich", Code = "MU", CountryId = 3 }
            );

        }

    }
}
