using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Repository
{
    public class HotelBookingRepository : IHotelBookingRepository
    {
        private readonly BookingContext _context;
        private readonly IMapper _mapper;

        public HotelBookingRepository(BookingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HotelBooking> CreateBookingAsync(HotelBooking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;

        }

        public async Task DeleteBookingAsync(int id)
        {
            HotelBooking booking =await _context.Bookings.FindAsync(id);
            if(booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<HotelBooking>> GetAllBookingsAsync()
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.ApplicationUser)
                .Include(h => h.Hotel)

                .ToListAsync();
        }

        public async Task<HotelBooking> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.ApplicationUser)
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<HotelBooking> UpdateBookingAsync(int id, HotelBooking booking)
        {
            HotelBooking existingBooking = await _context.Bookings.FindAsync(id);
            if (existingBooking == null)
                return null;

            _context.Entry(existingBooking).CurrentValues.SetValues(booking);
            await _context.SaveChangesAsync();
            return existingBooking;
        }
    }
}
