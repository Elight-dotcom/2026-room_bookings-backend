using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomBookingsApi.Services;

namespace RoomBookingsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusHistoryController : ControllerBase
    {
        private readonly IStatusHistoryService _statusHistoryService;

        public StatusHistoryController(IStatusHistoryService statusHistoryService)
        {
            _statusHistoryService = statusHistoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusHistoryByBookingId(int id)
        {
            var statusHistory = await _statusHistoryService.GetStatusHistoryById(id);
            return Ok(statusHistory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatusHistory()
        {
            var statusHistory = await _statusHistoryService.GetAllStatusHistory();
            return Ok(statusHistory);
        }
    }
}
