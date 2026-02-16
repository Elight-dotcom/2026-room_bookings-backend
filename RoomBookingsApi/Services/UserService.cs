using System;
using RoomBookingsApi.Data;
using RoomBookingsApi.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace RoomBookingsApi.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.Include(u => u.Role).ToListAsync();
    }

    public async Task<User?> GetUser(int id)
    {
        return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>> SearchUsers(string name)
    {
        return await _context.Users.Include(u => u.Role).Where(u => u.Name.ToLower().Contains(name.ToLower())).ToListAsync();
    }
}
