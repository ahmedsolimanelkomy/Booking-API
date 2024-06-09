using Booking_API.Models;
using Booking_API.Repository.IRepository;
using Booking_API.Repository;
using Booking_API.Services;
using Booking_API.Services.IService;
using Booking_API.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Booking_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
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

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<BookingContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<UserManager<ApplicationUser>>();
            builder.Services.AddScoped<IService<City>, Service<City>>();
            builder.Services.AddScoped<IService<Country>, Service<Country>>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add JWT Authentication
            var key = Encoding.ASCII.GetBytes(builder.Configuration["JWT:SecKey"]);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIss"],
                    ValidAudience = builder.Configuration["JWT:ValidAud"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            // Use Authentication and Authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
