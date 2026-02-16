using System;

namespace RoomBookingsApi.DTOs.BookingStatusHistory;

public class StatusHistoryResponseDto
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public int ChangedByUserId { get; set; }
    public string ChangedByUserName { get; set; } = null!;
    public int StatusId { get; set; }
    public string StatusName { get; set; } = null!;
    public string? Note { get; set; }
    public DateTime ChangedAt { get; set; }
}
