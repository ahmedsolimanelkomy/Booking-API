using Booking_API.Models;

namespace Booking_API.Repository.IRepository
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task AddAsync(Room entity);
    }
}
