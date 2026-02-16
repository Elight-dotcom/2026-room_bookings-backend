using System;

namespace RoomBookingsApi.Models;

public class BookingStatusHistory : BaseEntity
{
    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public int ChangedByUserId { get; set; }
    public User ChangedByUser { get; set; } = null!;

    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public string? Note { get; set; }

    public DateTime ChangedAt { get; set; }
}
