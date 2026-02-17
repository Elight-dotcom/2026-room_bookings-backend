using System;
using RoomBookingsApi.DTOs.Room;
using RoomBookingsApi.Models;

namespace RoomBookingsApi.Services;

public interface IRoomService
{
    Task<List<RoomResponseDto>> GetRooms();
    Task<RoomResponseDto?> GetRoom(int id);
}
