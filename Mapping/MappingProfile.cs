using AutoMapper;
using Booking_API.DTOs;
using Booking_API.DTOs.AccountDTOS;
using Booking_API.DTOs.AdminDTOS;
using Booking_API.DTOs.CarAgencyDTOS;
using Booking_API.DTOs.CarDTOS;
using Booking_API.DTOs.CarPhotoDTOS;
using Booking_API.DTOs.CarRental;
using Booking_API.DTOs.CityDTOS;
using Booking_API.DTOs.CountryDTOS;
using Booking_API.DTOs.FeatureDTOS;
using Booking_API.DTOs.HotelBookingDTOS;
using Booking_API.DTOs.HotelDTOS;
using Booking_API.DTOs.HotelPhotosDTOS;
using Booking_API.DTOs.InvoiceDTOS;
using Booking_API.DTOs.NewFolder;
using Booking_API.DTOs.RoomDTOS;
using Booking_API.Models;

namespace Booking_API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<HotelReview, DisplayHotelReviewDTO>();
            CreateMap<AddHotelReviewDTO, HotelReview>();

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
            CreateMap<CityDTO, City>()
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
            CreateMap<RoomTypeDTO, RoomType>().ReverseMap();
            CreateMap<Hotel, HotelDTO>();

            CreateMap<Hotel, FilteredHotelDTO>()
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos != null ? src.Photos.ToList() : null))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City != null ? src.City.Name : null))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating.HasValue ? src.Rating.Value : 0))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Rooms.Any() ? src.Rooms.First().RoomType.PricePerNight : 0))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();



            CreateMap<Room, FilteredRoomDTO>().ReverseMap();
            CreateMap<HotelBooking, HotelBookingViewDTO>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.BookingDate))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CheckIn, opt => opt.MapFrom(src => src.CheckInDate))
                .ForMember(dest => dest.CheckOut, opt => opt.MapFrom(src => src.CheckOutDate))
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.ApplicationUser.FirstName))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.ApplicationUser.LastName))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name))
                .ForMember(dest => dest.RoomNumbers, opt => opt.MapFrom(src => src.Rooms.Select(r => r.RoomNumber).ToList()))
                .ReverseMap()
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CheckInDate, opt => opt.MapFrom(src => src.CheckIn))
                .ForMember(dest => dest.CheckOutDate, opt => opt.MapFrom(src => src.CheckOut))
                .ForMember(dest => dest.ApplicationUser, opt => opt.MapFrom(src => new ApplicationUser
                {
                    FirstName = src.UserFirstName,
                    LastName = src.UserLastName
                }))
                .ForMember(dest => dest.Hotel, opt => opt.MapFrom(src => new Hotel
                {
                    Name = src.HotelName
                }))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.RoomNumbers.Select(rn => new Room { RoomNumber = rn })));


            CreateMap<Room, FilteredRoomHotelDTO>().ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.RoomType.Name))
                .ForMember(dest => dest.PricePerNight, opt => opt.MapFrom(src => src.RoomType.PricePerNight))
                .ReverseMap();

            CreateMap<CreateHotelBookingDTO, HotelBooking>().ReverseMap();
            CreateMap<HotelFilterDTO, HotelBookingViewDTO>().ReverseMap();
            CreateMap<Hotel, HotelViewDTO>()
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.City.Id)).ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();



            CreateMap<CarDTO, Car>().ReverseMap();
            CreateMap<CarAgency, CarAgencyDTO>().ReverseMap();
            CreateMap<CarRental, CarRentalDTO>().ReverseMap();

            CreateMap<HotelReview, DisplayHotelReviewDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.ApplicationUser.UserName))
            .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.ApplicationUser.PhotoUrl));

            CreateMap<HotelBookingInvoice, HotelBookingInvoiceDTO>().ReverseMap();
            CreateMap<CarRentalInvoice, CarRentalInvoiceDTO>().ReverseMap();
            CreateMap<ViewInvoiceDTO, HotelBookingInvoice>().ReverseMap();

            CreateMap<CarAgency, CarAgencyDTO>();
            CreateMap<CarAgency, CarAgencyViewDTO>();

            CreateMap<Hotel, FilteredHotelDTO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos))
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));

            CreateMap<Car, FilteredCarDTO>()
           .ForMember(dest => dest.CarPhotos, opt => opt.MapFrom(src => src.CarPhotos))
           .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.CarAgency.Name)).ReverseMap();
            


            CreateMap<CarRental, CarRentalViewDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RentDate, opt => opt.MapFrom(src => src.RentDate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.PickUpDate, opt => opt.MapFrom(src => src.PickUpDate))
                .ForMember(dest => dest.DropOffDate, opt => opt.MapFrom(src => src.DropOffDate))
                .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.ApplicationUser.FirstName))
                .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.ApplicationUser.LastName))
                .ForMember(dest => dest.CarAgencyName, opt => opt.MapFrom(src => src.CarAgency.Name))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Car.Brand));

            CreateMap<CarRental, CarRentalViewDTO>()
            .ForMember(dest => dest.UserFirstName, opt => opt.MapFrom(src => src.ApplicationUser.FirstName))
            .ForMember(dest => dest.UserLastName, opt => opt.MapFrom(src => src.ApplicationUser.LastName))
            .ForMember(dest => dest.CarAgencyName, opt => opt.MapFrom(src => src.CarAgency.Name))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Car.Brand))
            .ForMember(dest => dest.carPhotos, opt => opt.MapFrom(src => src.Car.CarPhotos));
        

            CreateMap<HotelWishListDTO, HotelWishList>().ReverseMap();

            CreateMap<Hotel, WishlistHotelDTO>()
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos != null ? src.Photos.ToList() : null))
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City != null ? src.City.Name : null))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Rooms.Any() ? src.Rooms.First().RoomType.PricePerNight : 0));

            CreateMap<UserUpdateDTO, ApplicationUser>().ReverseMap();
            CreateMap<UserViewDTO, ApplicationUser>().ReverseMap();

            CreateMap<PassportDto, Passport>()
                .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber)).ReverseMap();

            CreateMap<CreateCarRentDTO, CarRental>().ReverseMap();
            
            CreateMap<Car, FilteredCarDTO>()
           .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.CarAgency.Name))
           .ForMember(dest => dest.CarType, opt => opt.MapFrom(src => src.CarType))
           .ForMember(dest => dest.GasType, opt => opt.MapFrom(src => src.GasType));

            CreateMap<CarPhoto, CarPhotoDTO>();
            CreateMap<CarPhotoDTO, CarPhoto>();

            CreateMap<CarPhoto, GetCarPhotoDTO>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore()); // Assuming the photo content will be handled separately
            CreateMap<GetCarPhotoDTO, CarPhoto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore());

            CreateMap<CreateCarPhotoDTO, CarPhoto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore());
            CreateMap<CarPhoto, CreateCarPhotoDTO>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore());

            CreateMap<UpdateCarPhotoDTO, CarPhoto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore());
            CreateMap<CarPhoto, UpdateCarPhotoDTO>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore());

            CreateMap<BulkCarPhotoDTO, CarPhoto>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CarId, opt => opt.Ignore()); // Assuming each photo will be associated with a car individually

            CreateMap<Car, FilteredCarDTO>()
                .ForMember(dest => dest.GearType, opt => opt.MapFrom(src => src.GearType.ToString()))
                .ForMember(dest => dest.GasType, opt => opt.MapFrom(src => src.GasType.ToString()))
                .ForMember(dest => dest.CarPhotos, opt => opt.MapFrom(src => src.CarPhotos));

            CreateMap<CarPhoto, CarPhotoDTO>(); // If you have a CarPhotoDTO, map it here
        }

    }

}

