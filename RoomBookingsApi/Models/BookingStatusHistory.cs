using System;

namespace RoomBookingsApi.Models;

public class BookingStatusHistory : BaseEntity
{
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    public Guid ChangedBy { get; set; }
    public User ChangedByUser { get; set; } = null!;

    public Guid StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public string? Note { get; set; }

    public DateTime ChangedAt { get; set; }
}
