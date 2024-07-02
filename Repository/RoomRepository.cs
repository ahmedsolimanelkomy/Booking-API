using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private readonly BookingContext _dbcontext;
        private readonly DbSet<Room> _dbSet;

        public RoomRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
            _dbSet = _dbcontext.Set<Room>();
        }
        public override async Task AddAsync(Room entity)
        {
            entity.RoomNumber = await this.GenerateRoomNumberAsync();
            await _dbSet.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
        }
        private async Task<int> GenerateRoomNumberAsync()
        {
            var lastRoom = await _dbSet.OrderByDescending(r => r.RoomNumber).FirstOrDefaultAsync();
            int nextRoomNumber = lastRoom != null ? lastRoom.RoomNumber + 1 : 100;
            return nextRoomNumber;
        }
    }
}
