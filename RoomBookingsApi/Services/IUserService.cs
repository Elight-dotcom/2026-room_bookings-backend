using System;
using RoomBookingsApi.Models;

namespace RoomBookingsApi.Services;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
    Task<User?> GetUser(Guid id);
    Task<User> Add(User user);
    Task<User> Update(Guid id, User user);
    Task<bool> Delete(Guid id);
}
