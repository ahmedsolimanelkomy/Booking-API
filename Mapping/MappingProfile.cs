using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;

namespace Booking_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDTO, ApplicationUser>();
            CreateMap<HotelDTO, Hotel>();
            CreateMap<PassportDto, Passport>();
            CreateMap<Passport, PassportDto>();


        }
    }
}
