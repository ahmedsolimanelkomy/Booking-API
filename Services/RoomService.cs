using AutoMapper;
using Booking_API.DTOs.RoomDTOS;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class RoomService : Service<Room>, IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Room> AddDTOAsync(AddRoomDTO RoomDTO)
        {

            var Room = _mapper.Map<Room>(RoomDTO);

            await _unitOfWork.GetRepository<Room>().AddAsync(Room);
            await _unitOfWork.SaveAsync();

            return Room;
        }

        public async Task<Room> UpdateDTOAsync(AddRoomDTO RoomDTO)
        {
            var Room = await _unitOfWork.GetRepository<Room>().GetAsync(h => h.Id == RoomDTO.Id);

            if (Room == null)
            {
                throw new KeyNotFoundException("Room not found");
            }

            _mapper.Map(RoomDTO, Room);
            await base.UpdateAsync(Room);
            return Room;
        }
    }
}