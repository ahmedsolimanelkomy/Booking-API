using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.DTOs.RoomDTOS;
using Booking_API.Models;

namespace Booking_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDTO, ApplicationUser>().ReverseMap();
            CreateMap<AdminDTO, ApplicationUser>().ReverseMap();
            CreateMap<HotelDTO, Hotel>();
            CreateMap<PassportDto, Passport>().ReverseMap();
            CreateMap<RoomViewDTO, Room>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom(src => src.AvailabilityStatus))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.View, opt => opt.MapFrom(src => src.View))
            .ForMember(dest => dest.IsBooked, opt => opt.MapFrom(src => src.IsBooked))
            .ForPath(dest => dest.Hotel.Name, opt => opt.MapFrom(src => src.HotelName))
            .ForPath(dest => dest.RoomType.Name, opt => opt.MapFrom(src => src.TypeName))
            .ForPath(dest => dest.RoomType.PricePerNight, opt => opt.MapFrom(src => src.PricePerNight))
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom(src => src.AvailabilityStatus))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.View, opt => opt.MapFrom(src => src.View))
            .ForMember(dest => dest.IsBooked, opt => opt.MapFrom(src => src.IsBooked))
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.RoomType.Name))
            .ForMember(dest => dest.PricePerNight, opt => opt.MapFrom(src => src.RoomType.PricePerNight));

            CreateMap<Room, AddRoomDTO>()
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId != null ? (int?)src.HotelId : null))
                .ForMember(dest => dest.RoomTypeId, opt => opt.MapFrom(src => src.RoomTypeId != null ? (int?)src.RoomTypeId : null))
                .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom(src => src.AvailabilityStatus))
                .ForMember(dest => dest.IsBooked, opt => opt.MapFrom(src => src.IsBooked))
                .ReverseMap();

            CreateMap<HotelFeatureDTO, Feature>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ReverseMap();




        }
    }
}
