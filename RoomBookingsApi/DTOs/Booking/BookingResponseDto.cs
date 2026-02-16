using System;

namespace RoomBookingsApi.DTOs.Booking;

public class BookingResponseDto
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string UserNRP { get; set; } = null!;

    public int RoomId { get; set; }
    public string RoomName { get; set; } = null!;
    public string RoomLocation { get; set; } = null!;

    public DateOnly BookingDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Purpose { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public string StatusName { get; set; } = null!;
}
