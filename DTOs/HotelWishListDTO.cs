namespace Booking_API.DTOs
{
    public class HotelWishListDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public ICollection<int>? HotelIds { get; set; }
    }
}
