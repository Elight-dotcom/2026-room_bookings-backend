using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomBookingsApi.DTOs.Booking;
using RoomBookingsApi.Models;
using RoomBookingsApi.Services;

namespace RoomBookingsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingController : ControllerBase
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingController(IRoomBookingService roomBookingService)
        {
            _roomBookingService = roomBookingService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRoomBookings([FromQuery] string? searchTerm, [FromQuery] int? roomId, [FromQuery] DateOnly? bookingDate)
        {
            var roomBookings = await _roomBookingService.SearchRoomBookings(searchTerm, roomId, bookingDate);
            return Ok(roomBookings);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoomBookings()
        {
            var roomBookings = await _roomBookingService.GetAllRoomBookings();
            return Ok(roomBookings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomBooking(int id)
        {
            var roomBooking = await _roomBookingService.GetRoomBooking(id);
            if (roomBooking == null) return NotFound();
            return Ok(roomBooking);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoomBooking(AddBookingDto booking)
        {
            try
            {
                var roomBooking = await _roomBookingService.Add(booking);

                return CreatedAtAction(nameof(GetRoomBooking), new { id = roomBooking.Id }, roomBooking);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoomBooking(int id, UpdateBookingDto booking)
        {
            try
            {
                var updatedRoomBooking = await _roomBookingService.Update(id, booking);
                if (updatedRoomBooking == null) return NotFound();
                return Ok(updatedRoomBooking);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomBooking(int id)
        {
            var result = await _roomBookingService.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeBookingStatus(int id, ChangeStatusDto changeStatus)
        {
            try
            {
                var updatedBooking = await _roomBookingService.ChangeStatus(id, changeStatus);
                if (updatedBooking == null) return NotFound();
                return Ok(updatedBooking);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
