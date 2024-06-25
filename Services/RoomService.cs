using AutoMapper;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.DTOs.RoomDTOs;
using Booking_API.DTOs.RoomDTOS;
using Booking_API.Models;
using Booking_API.Repository;
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



        public async Task<IEnumerable<FilteredRoomDTO>> GetFilteredRoomsAsync(HotelFilterDTO filter)
        {
            var rooms = await _unitOfWork.Rooms.GetAllAsync(["HotelBooking", "RoomType", "Hotel"]);

            var filteredRooms = rooms.Where(room =>
                (!filter.CheckInDate.HasValue || !filter.CheckOutDate.HasValue ||
                    room.HotelBooking == null ||
                    room.HotelBooking.CheckOutDate <= filter.CheckInDate ||
                    room.HotelBooking.CheckInDate >= filter.CheckOutDate
                ) &&
                (!filter.MinPrice.HasValue || room.RoomType.PricePerNight >= filter.MinPrice) &&
                (!filter.MaxPrice.HasValue || room.RoomType.PricePerNight <= filter.MaxPrice) &&
                (!filter.RoomView.HasValue || room.View == filter.RoomView) &&
                (!filter.RoomTypeId.HasValue || room.RoomTypeId == filter.RoomTypeId) &&
                (!filter.RoomCapacity.HasValue || room.Capacity == filter.RoomCapacity)
            ).ToList();

            //var distinctHotels = filteredRooms.GroupBy(room => room.HotelId).Select(group => group.First().Hotel).ToList();

            return _mapper.Map<IEnumerable<FilteredRoomDTO>>(filteredRooms);
        } 

    }
}