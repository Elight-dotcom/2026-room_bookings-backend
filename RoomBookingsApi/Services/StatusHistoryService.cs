using RoomBookingsApi.Data;
using RoomBookingsApi.DTOs.BookingStatusHistory;
using Microsoft.EntityFrameworkCore;

namespace RoomBookingsApi.Services;

public class StatusHistoryService : IStatusHistoryService
{
    AppDbContext _context;

    public StatusHistoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<StatusHistoryResponseDto>> GetAllStatusHistory()
    {
        var statusHistory = await _context.BookingStatusHistories.Include(bsh => bsh.Status).Include(bsh => bsh.ChangedByUser).ToListAsync();
        return statusHistory.Select(bsh => new StatusHistoryResponseDto
        {
            Id = bsh.Id,
            BookingId = bsh.BookingId,
            ChangedByUserId = bsh.ChangedByUserId,
            ChangedByUserName = bsh.ChangedByUser.Name,
            StatusId = bsh.StatusId,
            StatusName = bsh.Status.Name,
            Note = bsh.Note,
            ChangedAt = bsh.ChangedAt
        }).ToList();
    }

    public async Task<List<StatusHistoryResponseDto>> GetStatusHistoryById(int id)
    {
        var statusHistory = await _context.BookingStatusHistories.Where(bsh => bsh.BookingId == id).Include(bsh => bsh.Status).Include(bsh => bsh.ChangedByUser).ToListAsync();
        return statusHistory.Select(bsh => new StatusHistoryResponseDto
        {
            Id = bsh.Id,
            BookingId = bsh.BookingId,
            ChangedByUserId = bsh.ChangedByUserId,
            ChangedByUserName = bsh.ChangedByUser.Name,
            StatusId = bsh.StatusId,
            StatusName = bsh.Status.Name,
            Note = bsh.Note,
            ChangedAt = bsh.ChangedAt
        }).ToList();
    }
}

