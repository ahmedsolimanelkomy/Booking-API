namespace Booking_API.Models
{
    public class Car
    {
        public string Model { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public bool AvailabilityStatus { get; set; }
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public TimeSpan PickUpTime { get; set; }
        public TimeSpan DropOffTime { get; set; }
        public bool InsuranceIncluded { get; set; }

        public bool IsBooked { get; set; }
        public string CarImage { get; set; }
        public int CarRating { get; set; }



    }
}
