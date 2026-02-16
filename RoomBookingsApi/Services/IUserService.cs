using System;
using RoomBookingsApi.Models;

namespace RoomBookingsApi.Services;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
    Task<User?> GetUser(int id);
}
