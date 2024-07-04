using AutoMapper;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;
using System.Linq.Expressions;

namespace Booking_API.Services
{
    public class HotelService : Service<Hotel>, IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Hotel> AddDTOAsync(HotelDTO hotelDTO)
        {
            
            var hotel = _mapper.Map<Hotel>(hotelDTO);
            await _unitOfWork.GetRepository<Hotel>().AddAsync(hotel);
            await _unitOfWork.SaveAsync();

            return hotel;
        }

        public async Task<Hotel> UpdateDTOAsync(HotelDTO hotelDTO)
        {
            var hotel = await _unitOfWork.GetRepository<Hotel>().GetAsync(h=>h.Id==hotelDTO.Id);
            if (hotel == null)
            {
                throw new KeyNotFoundException("Hotel not found");
            }

            _mapper.Map(hotelDTO, hotel);
            await base.UpdateAsync(hotel);
            return hotel;
        }

        //public async Task<IEnumerable<FilteredHotelDTO>> GetFilteredHotelsAsync(HotelFilterDTO filter)
        //{
        //    var hotels = await _unitOfWork.Hotels.GetAllAsync(new[] { "Rooms", "HotelBookings", "Rooms.HotelBooking", "Rooms.RoomType", "Features", "Photos", "City" });

        //    var filteredHotels = hotels.Where(hotel =>
        //        (!filter.CityId.HasValue || hotel.CityId == filter.CityId) &&
        //        (!filter.CheckInDate.HasValue || !filter.CheckOutDate.HasValue || hotel.Rooms.Any(room =>
        //            room.HotelBooking == null ||
        //            (room.HotelBooking.Status == BookingStatus.Completed || room.HotelBooking.Status != BookingStatus.Confirmed) ||
        //            room.AvailabilityStatus == true ||
        //            room.HotelBooking.CheckOutDate <= filter.CheckInDate ||
        //            room.HotelBooking.CheckInDate >= filter.CheckOutDate)) &&
        //        (!filter.MinPrice.HasValue || hotel.Rooms.Any(room => room.RoomType?.PricePerNight >= filter.MinPrice)) &&
        //        (!filter.MaxPrice.HasValue || hotel.Rooms.Any(room => room.RoomType?.PricePerNight <= filter.MaxPrice)) &&
        //        (!filter.RoomView.HasValue || hotel.Rooms.Any(room => room.View == filter.RoomView)) &&
        //        (filter.FeatureIds == null || !filter.FeatureIds.Any() || filter.FeatureIds.All(id => hotel.Features.Any(feature => feature.Id == id))) &&
        //        (!filter.RoomTypeId.HasValue || hotel.Rooms.Any(room => room.RoomTypeId == filter.RoomTypeId)) &&
        //        (!filter.RoomCapacity.HasValue || hotel.Rooms.Any(room => room.Capacity >= filter.RoomCapacity))
        //    ).ToList();

        //    // Filter out unavailable rooms inside each hotel
        //    foreach (var hotel in filteredHotels)
        //    {
        //        hotel.Rooms = hotel.Rooms.Where(room =>
        //            room.HotelBooking == null ||
        //            room.HotelBooking.Status == BookingStatus.Completed ||
        //            room.HotelBooking.CheckOutDate <= filter.CheckInDate ||
        //            room.HotelBooking.CheckInDate >= filter.CheckOutDate).ToList();
        //    }

        //    // Remove hotels without rooms
        //    filteredHotels = filteredHotels.Where(hotel => hotel.Rooms.Any()).ToList();

        //    return _mapper.Map<IEnumerable<FilteredHotelDTO>>(filteredHotels);
        //}

        public async Task<IEnumerable<FilteredHotelDTO>> GetFilteredHotelsAsync(HotelFilterDTO filter)
        {
            var hotels = await _unitOfWork.Hotels.GetAllAsync(new[] { "Rooms", "Rooms.HotelBookings", "Rooms.RoomType", "Features", "Photos", "City" });

            var filteredHotels = hotels.Where(hotel =>
                (!filter.CityId.HasValue || hotel.CityId == filter.CityId) &&
                (!filter.CheckInDate.HasValue || !filter.CheckOutDate.HasValue || hotel.Rooms.Any(room =>
                    !room.HotelBookings.Any() || // No bookings at all
                    room.HotelBookings.All(booking =>
                        booking.Status == BookingStatus.Completed || // Booking is completed
                        booking.Status != BookingStatus.Confirmed || // Booking is not confirmed
                        room.AvailabilityStatus == true || // Room is available
                        booking.CheckOutDate <= filter.CheckInDate || // Booking ends before check-in
                        booking.CheckInDate >= filter.CheckOutDate) // Booking starts after check-out
                )) &&
                (!filter.MinPrice.HasValue || hotel.Rooms.Any(room => room.RoomType?.PricePerNight >= filter.MinPrice)) &&
                (!filter.MaxPrice.HasValue || hotel.Rooms.Any(room => room.RoomType?.PricePerNight <= filter.MaxPrice)) &&
                (!filter.RoomView.HasValue || hotel.Rooms.Any(room => room.View == filter.RoomView)) &&
                (filter.FeatureIds == null || !filter.FeatureIds.Any() || filter.FeatureIds.All(id => hotel.Features.Any(feature => feature.Id == id))) &&
                (!filter.RoomTypeId.HasValue || hotel.Rooms.Any(room => room.RoomTypeId == filter.RoomTypeId)) &&
                (!filter.RoomCapacity.HasValue || hotel.Rooms.Any(room => room.Capacity >= filter.RoomCapacity))
            ).ToList();

            // Filter out unavailable rooms inside each hotel
            foreach (var hotel in filteredHotels)
            {
                hotel.Rooms = hotel.Rooms.Where(room =>
                    !room.HotelBookings.Any() ||
                    room.HotelBookings.All(booking =>
                        booking.Status == BookingStatus.Completed ||
                        booking.CheckOutDate <= filter.CheckInDate ||
                        booking.CheckInDate >= filter.CheckOutDate)
                ).ToList();
            }

            // Remove hotels without rooms
            filteredHotels = filteredHotels.Where(hotel => hotel.Rooms.Any()).ToList();

            return _mapper.Map<IEnumerable<FilteredHotelDTO>>(filteredHotels);
        }



    }
}