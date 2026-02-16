using System;
using RoomBookingsApi.DTOs.Booking;
using RoomBookingsApi.Models;

namespace RoomBookingsApi.Services;

public interface IRoomBookingService
{
    Task<List<BookingResponseDto>> GetAllRoomBookings();
    Task<BookingResponseDto?> GetRoomBooking(int id);
    Task<BookingResponseDto> Add(AddBookingDto booking);
    Task<BookingResponseDto> Update(int id, UpdateBookingDto booking);
    Task<bool> Delete(int id);
}
