using Booking_API.Models;

namespace Booking_API.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Hotel> Hotels { get; }
        IRepository<Room> Rooms { get; }
        IRepository<Flight> Flights { get; }
        IRepository<Booking> Bookings { get; }
        IRepository<Airline> Airlines { get; }
        IRepository<CarAgency> CarAgencies { get; }
        IRepository<Car> Cars { get; }
        IRepository<City> Cities { get; }
        IRepository<Country> Countries { get; }
        IRepository<Passport> Passports { get; }
        IRepository<Payment> Payments { get; }
        IRepository<Review> Reviews { get; }
        IRepository<RoomType> RoomTypes { get; }
        IRepository<Ticket> Tickets { get; }
        IRepository<WishList> WishLists { get; }

        IRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveAsync();
    }

}
