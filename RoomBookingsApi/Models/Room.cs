namespace RoomBookingsApi.Models;

public class Room : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
