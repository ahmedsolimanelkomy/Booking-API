using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class HotelPhotoService : Service<HotelPhoto>, IHotelPhotoService
    {
        public HotelPhotoService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
