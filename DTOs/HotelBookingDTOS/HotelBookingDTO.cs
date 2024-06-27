﻿using Booking_API.Models;
using Booking_API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelBookingDTOS
{
    public class HotelBookingDto
    {
        public int Id { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public BookingStatus Status { get; set; }

        [Required]
        [Range(0, double.MaxValue), DataType(DataType.Currency, ErrorMessage = "Total Price must be a positive number")]
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }
        [CheckInDate]
        public DateTime CheckInDate { get; set; }
        [CheckOutDate("CheckInDate", ErrorMessage = "Check-out date must be after the check-in date.")]
        public DateTime CheckOutDate { get; set; }
    }
}
