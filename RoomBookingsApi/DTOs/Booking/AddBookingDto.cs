using System;
using System.ComponentModel.DataAnnotations;

namespace RoomBookingsApi.DTOs.Booking;

public class AddBookingDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public DateOnly BookingDate { get; set; }
    [Required]
    public TimeOnly StartTime { get; set; }
    [Required]
    public TimeOnly EndTime { get; set; }

    [Required]
    public string Purpose { get; set; } = string.Empty;
}
