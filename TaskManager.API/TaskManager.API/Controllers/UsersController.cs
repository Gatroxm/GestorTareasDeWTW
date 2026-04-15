using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<User>> Create([FromBody] User user)
    {
        if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email))
        {
            return BadRequest("El nombre y el email son obligatorios.");
        }

        var newUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetAll), new { id = newUser.Id }, newUser);
    }
}