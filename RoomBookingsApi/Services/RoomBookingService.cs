using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RoomBookingsApi.Data;
using RoomBookingsApi.DTOs.Booking;
using RoomBookingsApi.Models;

namespace RoomBookingsApi.Services;

public class RoomBookingService : IRoomBookingService
{
    private readonly AppDbContext _context;

    public RoomBookingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookingResponseDto>> SearchRoomBookings(string? searchTerm, int? roomId, DateOnly? bookingDate)
    {
        var query = _context.Bookings.Include(rb => rb.Room).Include(rb => rb.User).AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(b => b.User.Name.ToLower().Contains(searchTerm.ToLower()));
        }

        if (roomId.HasValue)
        {
            query = query.Where(b => b.RoomId == roomId.Value);
        }

        if (bookingDate.HasValue)
        {
            query = query.Where(b => b.BookingDate == bookingDate.Value);
        }

        var bookings = await query.ToListAsync();

        return bookings.Select(booking => new BookingResponseDto
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            RoomName = booking.Room.Name,
            RoomLocation = booking.Room.Location,
            UserId = booking.UserId,
            UserName = booking.User.Name,
            UserNRP = booking.User.NRP,
            BookingDate = booking.BookingDate,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Purpose = booking.Purpose,
            StatusId = booking.StatusId
        }).ToList();
    }

    public async Task<List<BookingResponseDto>> GetAllRoomBookings()
    {
        var bookings = await _context.Bookings.Include(rb => rb.Room).Include(rb => rb.User).ToListAsync();
        return bookings.Select(booking => new BookingResponseDto
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            RoomName = booking.Room.Name,
            RoomLocation = booking.Room.Location,
            UserId = booking.UserId,
            UserName = booking.User.Name,
            UserNRP = booking.User.NRP,
            BookingDate = booking.BookingDate,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Purpose = booking.Purpose,
            StatusId = booking.StatusId
        }).ToList();
    }

    public async Task<BookingResponseDto?> GetRoomBooking(int id)
    {
        var booking = await _context.Bookings.Include(rb => rb.Room).Include(rb => rb.User).FirstOrDefaultAsync(b => b.Id == id);
        if (booking == null) return null;

        return new BookingResponseDto
        {
            Id = booking.Id,
            RoomId = booking.RoomId,
            RoomName = booking.Room.Name,
            RoomLocation = booking.Room.Location,
            UserId = booking.UserId,
            UserName = booking.User.Name,
            UserNRP = booking.User.NRP,
            BookingDate = booking.BookingDate,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Purpose = booking.Purpose,
            StatusId = booking.StatusId
        };
    }

    public async Task<BookingResponseDto> Add(AddBookingDto booking)
    {
        var newBooking = new Booking
        {
            RoomId = booking.RoomId,
            UserId = booking.UserId,
            BookingDate = booking.BookingDate,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            Purpose = booking.Purpose,
            StatusId = 1
        };

        _context.Bookings.Add(newBooking);
        await _context.SaveChangesAsync();

        return new BookingResponseDto
        {
            Id = newBooking.Id,
            RoomId = newBooking.RoomId,
            UserId = newBooking.UserId,
            BookingDate = newBooking.BookingDate,
            StartTime = newBooking.StartTime,
            EndTime = newBooking.EndTime,
            Purpose = newBooking.Purpose,
            StatusId = newBooking.StatusId,
        };
    }

    public async Task<BookingResponseDto> Update(int id, UpdateBookingDto booking)
    {
        var existingBooking = await _context.Bookings.FindAsync(id);
        if (existingBooking == null) throw new InvalidOperationException("Booking not found");

        if (existingBooking.StatusId != 1) throw new InvalidOperationException("Only pending bookings can be updated");
        existingBooking.RoomId = booking.RoomId;
        existingBooking.UserId = booking.UserId;
        existingBooking.BookingDate = booking.BookingDate;
        existingBooking.StartTime = booking.StartTime;
        existingBooking.EndTime = booking.EndTime;
        existingBooking.Purpose = booking.Purpose;

        await _context.SaveChangesAsync();
        return new BookingResponseDto
        {
            Id = existingBooking.Id,
            RoomId = existingBooking.RoomId,
            UserId = existingBooking.UserId,
            BookingDate = existingBooking.BookingDate,
            StartTime = existingBooking.StartTime,
            EndTime = existingBooking.EndTime,
            Purpose = existingBooking.Purpose,
            StatusId = existingBooking.StatusId
        };
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
