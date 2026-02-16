using System;
using System.ComponentModel.DataAnnotations;

namespace RoomBookingsApi.DTOs.Booking;

public class UpdateBookingDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public DateTime BookingDate { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public string Purpose { get; set; } = string.Empty;
}
