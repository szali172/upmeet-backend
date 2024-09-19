using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UpmeetBackend.Data;
using UpmeetBackend.Data.Dto;
using UpmeetBackend.Models;

namespace UpmeetBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private UpmeetDbContext _upmeetDbContext;

    public UsersController(UpmeetDbContext upmeetDbContext)
    {
        _upmeetDbContext = upmeetDbContext;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await _upmeetDbContext.Users.ToListAsync());
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var userEntity = await _upmeetDbContext.Users.FindAsync(id);

        if (userEntity == null)
        {
            return NotFound();
        }

        return Ok(userEntity);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {

        if (user == null || string.IsNullOrEmpty(user.UserName))
        {
            return BadRequest("Invalid user data.");
        }

        User newUser = new User
        {
            UserName = user.UserName 
        };

        _upmeetDbContext.Add(newUser);

        await _upmeetDbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateUser), new { id = newUser.UserId }, newUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var userEntity = await _upmeetDbContext.Users.FindAsync(id);

        if (userEntity == null)
        {
            return NotFound();
        }

        _upmeetDbContext.Users.Remove(userEntity);
        await _upmeetDbContext.SaveChangesAsync();

        return NoContent();
    }
}
