using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Repository
{
    public class HotelBookingRepository : Repository<HotelBooking>, IHotelBookingRepository
    {
        private readonly BookingContext _dbcontext;
        public HotelBookingRepository(BookingContext dBcontext) : base(dBcontext)
        {
            _dbcontext = dBcontext;
        }
    }
}
