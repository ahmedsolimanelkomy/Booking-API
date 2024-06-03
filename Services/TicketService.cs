using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class TicketService : Service<Ticket>, ITicketService
    {
        public TicketService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
