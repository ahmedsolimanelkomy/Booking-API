using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingContext _context;

        public IRepository<Hotel> Hotels { get; private set; }
        public IRepository<Room> Rooms { get; private set; }
        public IRepository<Flight> Flights { get; private set; }
        public IRepository<Booking> Bookings { get; private set; }
        public IRepository<Airline> Airlines { get; private set; }
        public IRepository<CarAgency> CarAgencies { get; private set; }
        public IRepository<Car> Cars { get; private set; }
        public IRepository<City> Cities { get; private set; }
        public IRepository<Country> Countries { get; private set; }
        public IRepository<Passport> Passports { get; private set; }
        public IRepository<Payment> Payments { get; private set; }
        public IRepository<Review> Reviews { get; private set; }
        public IRepository<RoomType> RoomTypes { get; private set; }
        public IRepository<Ticket> Tickets { get; private set; }
        public IRepository<WishList> WishLists { get; private set; }
        public IRepository<ApplicationUser> ApplicationUsers { get; private set; }

        public UnitOfWork(BookingContext context)
        {
            _context = context;
            Hotels = new Repository<Hotel>(_context);
            Rooms = new Repository<Room>(_context);
            Flights = new Repository<Flight>(_context);
            Bookings = new Repository<Booking>(_context);
            Airlines = new Repository<Airline>(_context);
            CarAgencies = new Repository<CarAgency>(_context);
            Cars = new Repository<Car>(_context);
            Cities = new Repository<City>(_context);
            Countries = new Repository<Country>(_context);
            Passports = new Repository<Passport>(_context);
            Payments = new Repository<Payment>(_context);
            Reviews = new Repository<Review>(_context);
            RoomTypes = new Repository<RoomType>(_context);
            Tickets = new Repository<Ticket>(_context);
            WishLists = new Repository<WishList>(_context);
            ApplicationUsers = new Repository<ApplicationUser>(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
