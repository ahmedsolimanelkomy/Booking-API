using Booking_API.Models;
using Booking_API.Repository.IRepository;

namespace Booking_API.Repository
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        private readonly BookingContext _dbcontext;
        public TicketRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
