﻿using System.ComponentModel.DataAnnotations;

namespace Booking_API.DTOs.HotelPhotosDTOS
{
    public class HotelPhotoDTO
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
