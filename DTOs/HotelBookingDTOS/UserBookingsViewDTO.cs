namespace Booking_API.DTOs.HotelBookingDTOS
{
    public class UserBookingsViewDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string Notes { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string HotelName { get; set; }
        public List<int> RoomNumbers { get; set; }
    }
}
