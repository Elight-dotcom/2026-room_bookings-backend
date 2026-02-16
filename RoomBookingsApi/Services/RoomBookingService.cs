using System;
using Microsoft.EntityFrameworkCore;
using RoomBookingsApi.Data;
using RoomBookingsApi.Models;

namespace RoomBookingsApi.Services;

public class RoomBookingService : IRoomBookingService
{
    private readonly AppDbContext _context;

    public RoomBookingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Booking>> GetAllRoomBookings()
    {
        return await _context.Bookings.Include(rb => rb.Room).Include(rb => rb.User).ToListAsync();
    }

    public async Task<Booking?> GetRoomBooking(int id)
    {
        return await _context.Bookings.Include(rb => rb.Room).Include(rb => rb.User).FirstOrDefaultAsync(rb => rb.Id == id);
    }

    public async Task<Booking> Add(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task<Booking> Update(int id, Booking booking)
    {
        var existingBooking = await _context.Bookings.FindAsync(id);
        if (existingBooking == null) throw new InvalidOperationException("Booking not found");

        existingBooking.RoomId = booking.RoomId;
        existingBooking.UserId = booking.UserId;
        existingBooking.StartTime = booking.StartTime;
        existingBooking.EndTime = booking.EndTime;

        await _context.SaveChangesAsync();
        return existingBooking;
    }

    public async Task<bool> Delete(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return false;

        _context.Bookings.Remove(booking);
        await _context.SaveChangesAsync();
        return true;
    }
}
