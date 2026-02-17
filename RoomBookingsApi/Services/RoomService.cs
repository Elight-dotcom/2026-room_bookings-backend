using RoomBookingsApi.Models;
using RoomBookingsApi.Data;
using Microsoft.EntityFrameworkCore;
using RoomBookingsApi.DTOs.Room;
namespace RoomBookingsApi.Services;

public class RoomService : IRoomService
{
    private readonly AppDbContext _context;

    public RoomService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoomResponseDto>> GetRooms()
    {
        var rooms = await _context.Rooms.ToListAsync();
        return rooms.Select(u => new RoomResponseDto
        {
            Id = u.Id,
            Name = u.Name,
            Capacity = u.Capacity,
            Location = u.Location,
            Description = u.Description,
            CreatedAt = u.CreatedAt ?? DateTime.UtcNow
        }).ToList();
    }

    public async Task<RoomResponseDto?> GetRoom(int id)
    {
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        if (room == null) return null;

        return new RoomResponseDto
        {
            Id = room.Id,
            Name = room.Name,
            Capacity = room.Capacity,
            Location = room.Location,
            Description = room.Description,
            CreatedAt = room.CreatedAt ?? DateTime.UtcNow
        };
    }
}