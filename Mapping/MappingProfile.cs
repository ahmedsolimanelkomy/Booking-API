using AutoMapper;
using Booking_API.DTOs;
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
            CreateMap<RoomDTO, Room>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom(src => src.AvailabilityStatus))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.View, opt => opt.MapFrom(src => src.View))
            .ForMember(dest => dest.Hotel.Name, opt => opt.MapFrom(src => src.HotelName))
            .ForMember(dest => dest.RoomType.Name, opt => opt.MapFrom(src => src.TypeName))
            .ForMember(dest => dest.RoomType.PricePerNight, opt => opt.MapFrom(src => src.PricePerNight));

        }
    }
}
