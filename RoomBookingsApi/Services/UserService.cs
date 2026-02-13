using System;
using RoomBookingsApi.Data;
using RoomBookingsApi.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<User?> GetUser(Guid id)
    {
        return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> Add(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> Update(Guid id, User user)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null) throw new InvalidOperationException("User not found");

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.RoleId = user.RoleId;

        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<bool> Delete(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}
