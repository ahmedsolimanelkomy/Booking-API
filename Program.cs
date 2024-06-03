
using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Repository;
using Microsoft.EntityFrameworkCore;
using Booking_API.Services;
using Microsoft.AspNetCore.Identity;

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
            builder.Services.AddScoped<BookingService, BookingService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<BookingContext>()
        .AddDefaultTokenProviders();

            builder.Services.AddScoped<UserManager<ApplicationUser>>();



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
