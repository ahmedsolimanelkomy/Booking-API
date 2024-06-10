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
<<<<<<< HEAD
            CreateMap<PassportDto, Passport>().ReverseMap();


=======
            CreateMap<RoomDTO, Room>();
            CreateMap<PassportDto, Passport>();
            CreateMap<Passport, PassportDto>();
            
>>>>>>> c0cf980c6cb63f78cf498cce24bc5c1f40895346


        }
    }
}
