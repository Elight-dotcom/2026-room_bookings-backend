using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomBookingsApi.DTOs.User;
using RoomBookingsApi.Models;
using RoomBookingsApi.Services;

namespace RoomBookingsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string? searchTerm)
        {
            List<User> users;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                users = await _userService.GetAllUsers();
            }
            else
            {
                users = await _userService.SearchUsers(searchTerm);
            }

            var result = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                NRP = u.NRP,
                Email = u.Email,
                Role = u.Role.Name,
                CreatedAt = u.CreatedAt ?? DateTime.UtcNow
            });

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            var result = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                NRP = u.NRP,
                Email = u.Email,
                Role = u.Role.Name,
                CreatedAt = u.CreatedAt ?? DateTime.UtcNow
            });

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUser(id);

            if (user == null) return NotFound();
            var result = new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                NRP = user.NRP,
                Email = user.Email,
                Role = user.Role.Name,
                CreatedAt = user.CreatedAt ?? DateTime.UtcNow
            };

            return Ok(result);
        }
    }
}
