using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class RoomService : Service<Room>, IRoomService
    {
        public RoomService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}