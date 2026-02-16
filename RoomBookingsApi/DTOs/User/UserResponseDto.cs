using System;

namespace RoomBookingsApi.DTOs.User;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Role { get; set; } = null!;

    public string NRP { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
