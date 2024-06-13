﻿using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class CarService : Service<Car>, ICarService
    {
        public CarService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }

}

