using System;
using System.ComponentModel.DataAnnotations;

namespace RoomBookingsApi.DTOs.Booking;

public class ChangeStatusDto
{
    [Required]
    public int StatusId { get; set; }
    public int UserId { get; set; }

    public string? Note { get; set; }
}
