using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingContext _context;
        private Dictionary<Type, object> _repositories;


        public IRepository<Hotel> Hotels { get; private set; }
        public IRepository<Feature> Features { get; private set; }
        public IRepository<HotelPhoto> HotelPhotos { get; private set; }
        public IRepository<Room> Rooms { get; private set; }
        public IRepository<HotelBooking> HotelBookings { get; private set; }
        public IRepository<CarAgency> CarAgencies { get; private set; }
        public IRepository<Car> Cars { get; private set; }
        public IRepository<City> Cities { get; private set; }
        public IRepository<Country> Countries { get; private set; }
        public IRepository<Passport> Passports { get; private set; }
        public IRepository<HotelReview> Reviews { get; private set; }
        public IRepository<RoomType> RoomTypes { get; private set; }
        public IRepository<HotelWishList> WishLists { get; private set; }
        public IRepository<ApplicationUser> ApplicationUsers { get; private set; }
        public IRepository<CarRentalInvoice> CarRentalInvoices { get; private set; }
        public IRepository<CarRental> CarRental { get; private set; }
        public IRepository<HotelBookingInvoice> HotelBookingInvoices { get; private set; }

        public UnitOfWork(BookingContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();

            Hotels = new Repository<Hotel>(_context);
            Features = new Repository<Feature>(_context);
            HotelPhotos = new Repository<HotelPhoto>(_context);
            Rooms = new Repository<Room>(_context);
            HotelBookings = new Repository<HotelBooking>(_context);
            CarAgencies = new Repository<CarAgency>(_context);
            Cars = new Repository<Car>(_context);
            Cities = new Repository<City>(_context);
            Countries = new Repository<Country>(_context);
            Passports = new Repository<Passport>(_context);
            Reviews = new Repository<HotelReview>(_context);
            RoomTypes = new Repository<RoomType>(_context);
            WishLists = new Repository<HotelWishList>(_context);
            ApplicationUsers = new Repository<ApplicationUser>(_context);
            CarRentalInvoices = new Repository<CarRentalInvoice>(_context);
            CarRental = new Repository<CarRental>(_context);
            HotelBookingInvoices = new Repository<HotelBookingInvoice>(_context);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repositoryInstance = new Repository<T>(_context);
                _repositories.Add(typeof(T), repositoryInstance);
            }

            return (IRepository<T>)_repositories[typeof(T)];
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
