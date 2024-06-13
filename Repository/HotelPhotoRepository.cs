using AutoMapper;
using Booking_API.DTOs;
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Booking_API.Repository
{
    public class HotelPhotoRepository : Repository<HotelPhoto>, IHotelPhotoRepository
    {
        private readonly BookingContext _dbContext;
        private readonly IMapper _mapper;
        public HotelPhotoRepository(BookingContext context, IMapper mapper) : base(context)
        {
            _dbContext = context;
            _mapper = mapper;
        }


        public async Task AddAsync(HotelPhoto entity)
        {
            await _dbContext.HotelPhotos.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(HotelPhoto entity)
        {
            _dbContext.HotelPhotos.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.HotelPhotos.FindAsync(id);
            if (entity != null)
            {
                _dbContext.HotelPhotos.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }



        public async Task<IEnumerable<HotelPhotoDTO>> GetByHotelIdAsync(int hotelId)
        {
            var photos = await _dbContext.HotelPhotos.Where(photo => photo.HotelId == hotelId).ToListAsync();
            return _mapper.Map<IEnumerable<HotelPhotoDTO>>(photos);
        }





    }
}
