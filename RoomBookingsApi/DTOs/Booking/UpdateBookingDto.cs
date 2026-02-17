using System;
using System.ComponentModel.DataAnnotations;

namespace RoomBookingsApi.DTOs.Booking;

public class UpdateBookingDto
{
    public int UserId { get; set; }
    public int RoomId { get; set; }

    public DateOnly BookingDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public string Purpose { get; set; } = string.Empty;
}
