﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Booking_API.Models
{
    public enum InvoiceStatus
    {
        Pending,
        Paid,
        Cancelled
    }

    public class HotelBookingInvoice
    {
        [Key]
        public int Id { get; set; }

        public int Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public InvoiceStatus Status { get; set; }

        public string PaymentMethod { get; set; }

        public string TransactionId { get; set; }

        // Foreign Keys
       

        [ForeignKey("User")]
        public int ApplicationUserId { get; set; }

        // Navigation Properties

        public ApplicationUser User { get; set; }
        public ICollection<HotelBookingInvoice>? HotelBookingInvoices { get; set; } = new List<HotelBookingInvoice>();

    }
}
