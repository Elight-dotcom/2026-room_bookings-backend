using System;
using RoomBookingsApi.Models;

namespace RoomBookingsApi.Services;

public interface IRoomBookingService
{
    Task<List<Booking>> GetAllRoomBookings();
    Task<Booking?> GetRoomBooking(int id);
    Task<Booking> Add(Booking booking);
    Task<Booking> Update(int id, Booking booking);
    Task<bool> Delete(int id);
}
