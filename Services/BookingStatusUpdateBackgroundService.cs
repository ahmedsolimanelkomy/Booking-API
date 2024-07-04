using Booking_API.Services.IService;

namespace Booking_API.Services
{
    public class BookingStatusUpdateBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<BookingStatusUpdateBackgroundService> _logger;

        public BookingStatusUpdateBackgroundService(IServiceProvider serviceProvider, ILogger<BookingStatusUpdateBackgroundService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await UpdateBookingStatusAsync(stoppingToken); // Initial run on startup

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken); // Adjust the delay as needed
                await UpdateBookingStatusAsync(stoppingToken);
            }
        }

        private async Task UpdateBookingStatusAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bookingStatusService = scope.ServiceProvider.GetRequiredService<IHotelBookingService>();

                try
                {
                    await bookingStatusService.UpdateBookingStatusAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating booking statuses.");
                }
            }
        }
    }

}
