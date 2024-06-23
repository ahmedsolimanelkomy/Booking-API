using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.AdminDTOS;
using Booking_API.DTOs.FeatureDTOS;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.DTOs.HotelPhotosDTOS;
using Booking_API.DTOs.NewFolder;
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
            CreateMap<CreateAdminDTO, ApplicationUser>().ReverseMap();
            CreateMap<HotelDTO, Hotel>();
            CreateMap<HotelPhotoDTO, HotelPhoto>().ReverseMap();
            CreateMap<CreateHotelPhotoDTO, HotelPhoto>().ReverseMap();
            CreateMap<GetHotelPhotoDTO, HotelPhoto>().ReverseMap();
            CreateMap<CreateHotelPhotoDTO, HotelPhotoDTO>().ReverseMap();
            CreateMap<PassportDto, Passport>().ReverseMap();
            CreateMap<CountryDTO, Country>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();
            CreateMap<CityDTO,City>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code)).ReverseMap();

            CreateMap<RoomViewDTO, Room>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom(src => src.AvailabilityStatus))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.View, opt => opt.MapFrom(src => src.View))
            .ForPath(dest => dest.Hotel.Name, opt => opt.MapFrom(src => src.HotelName))
            .ForPath(dest => dest.RoomType.Name, opt => opt.MapFrom(src => src.TypeName))
            .ForPath(dest => dest.RoomType.PricePerNight, opt => opt.MapFrom(src => src.PricePerNight))
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom(src => src.AvailabilityStatus))
            .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.View, opt => opt.MapFrom(src => src.View))
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.RoomType.Name))
            .ForMember(dest => dest.PricePerNight, opt => opt.MapFrom(src => src.RoomType.PricePerNight));

            CreateMap<Room, AddRoomDTO>()
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId != null ? (int?)src.HotelId : null))
                .ForMember(dest => dest.RoomTypeId, opt => opt.MapFrom(src => src.RoomTypeId != null ? (int?)src.RoomTypeId : null))
                .ForMember(dest => dest.AvailabilityStatus, opt => opt.MapFrom(src => src.AvailabilityStatus))
                .ReverseMap();

            CreateMap<HotelFeatureDTO, Feature>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                 .ReverseMap();

            CreateMap<FeatureDTO, Feature>().ReverseMap();
            CreateMap<RoomTypeDTO,RoomType>().ReverseMap();
            CreateMap<Hotel, HotelDTO>();

            CreateMap<Hotel, FilteredHotelDTO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ReverseMap();



        }
    }
}
