using System;
using System.ComponentModel.DataAnnotations;

namespace RoomBookingsApi.DTOs.Booking;

public class ChangeBookingStatusDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public int StatusId { get; set; }
}
