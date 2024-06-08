﻿using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Booking_API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingContext _context;
        private Dictionary<Type, object> _repositories;


        public IRepository<Hotel> Hotels { get; private set; }
        public IRepository<Room> Rooms { get; private set; }
        public IRepository<Booking> Bookings { get; private set; }
        public IRepository<CarAgency> CarAgencies { get; private set; }
        public IRepository<Car> Cars { get; private set; }
        public IRepository<City> Cities { get; private set; }
        public IRepository<Country> Countries { get; private set; }
        public IRepository<Passport> Passports { get; private set; }
        public IRepository<Payment> Payments { get; private set; }
        public IRepository<Review> Reviews { get; private set; }
        public IRepository<RoomType> RoomTypes { get; private set; }
        public IRepository<WishList> WishLists { get; private set; }
        public IRepository<ApplicationUser> ApplicationUsers { get; private set; }

        public UnitOfWork(BookingContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();

            Hotels = new Repository<Hotel>(_context);
            Rooms = new Repository<Room>(_context);
            Bookings = new Repository<Booking>(_context);
            CarAgencies = new Repository<CarAgency>(_context);
            Cars = new Repository<Car>(_context);
            Cities = new Repository<City>(_context);
            Countries = new Repository<Country>(_context);
            Passports = new Repository<Passport>(_context);
            Payments = new Repository<Payment>(_context);
            Reviews = new Repository<Review>(_context);
            RoomTypes = new Repository<RoomType>(_context);
            WishLists = new Repository<WishList>(_context);
            ApplicationUsers = new Repository<ApplicationUser>(_context);
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repositoryInstance = new Repository<T>(_context);
                _repositories.Add(typeof(T), repositoryInstance);
            }

            return (IRepository<T>)_repositories[typeof(T)];
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
