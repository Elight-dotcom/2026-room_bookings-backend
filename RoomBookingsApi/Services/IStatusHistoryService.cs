using System;
using RoomBookingsApi.DTOs.BookingStatusHistory;

namespace RoomBookingsApi.Services;

public interface IStatusHistoryService
{
    Task<List<StatusHistoryResponseDto>> GetAllStatusHistory();
    Task<List<StatusHistoryResponseDto>> GetStatusHistoryById(int id);
}
