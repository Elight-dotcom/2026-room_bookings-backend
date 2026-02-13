namespace RoomBookingsApi.Models;

public class Booking : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public Guid RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public DateOnly BookingDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public string Purpose { get; set; } = string.Empty;

    public Guid StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public List<BookingStatusHistory> StatusHistory { get; set; } = new();
}
