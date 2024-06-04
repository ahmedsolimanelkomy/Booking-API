using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class RoomTypeService : Service<RoomType>, IRoomTypeService
    {
        public RoomTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}