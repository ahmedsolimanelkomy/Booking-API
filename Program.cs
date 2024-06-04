
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Repository;
using Microsoft.EntityFrameworkCore;
using Booking_API.Services;
using Microsoft.AspNetCore.Identity;
using Booking_API.Services.IService;
using Booking_API.Mapping;

namespace Booking_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<BookingContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IAirlineService, AirlineService>();
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            builder.Services.AddScoped< IPaymentService, PaymentService> ();
            builder.Services.AddScoped<IPassportService, PassportService>();
            builder.Services.AddScoped<IWishListService, WishListService>();
            builder.Services.AddScoped<IHotelService, HotelService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<BookingContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<UserManager<ApplicationUser>>();
            builder.Services.AddScoped<IService<City>, Service<City>>();
            builder.Services.AddScoped<IService<Country>, Service<Country>>();

            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
