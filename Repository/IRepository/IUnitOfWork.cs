using Booking_API.Models;

namespace Booking_API.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Hotel> Hotels { get; }
        IRepository<HotelPhoto> HotelPhotos { get; }
        IRepository<Feature> Features { get; }
        IRepository<Room> Rooms { get; }
        IRepository<HotelBooking> Bookings { get; }
        IRepository<CarAgency> CarAgencies { get; }
        IRepository<Car> Cars { get; }
        IRepository<City> Cities { get; }
        IRepository<Country> Countries { get; }
        IRepository<Passport> Passports { get; }
        IRepository<Payment> Payments { get; }
        IRepository<HotelReview> Reviews { get; }
        IRepository<RoomType> RoomTypes { get; }
        IRepository<HotelWishList> WishLists { get; }

        IRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveAsync();
    }

}
