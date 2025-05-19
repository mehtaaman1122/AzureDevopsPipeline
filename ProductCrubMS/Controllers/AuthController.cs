using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCrubMS.Models;
using System;
using System.Threading.Tasks;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    // ✅ LOGIN
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequests request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || user.Password != request.Password)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        if (!user.IsActive)
        {
            return Unauthorized(new { message = "User account is inactive. Contact support." });
        }

        // ✅ Update LastLogin
        user.LastLogin = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Login successful",
            userId = user.Id,
            name = user.Name,
            lastLogin = user.LastLogin
        });
    }

    // Controllers/AuthController.cs

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        var user = await _context.Users.FindAsync(request.Id);

        if (user == null)
            return NotFound(new { message = "User not found" });

        if (!string.IsNullOrWhiteSpace(request.Name))
            user.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.Password))
            user.Password = request.Password;

        if (request.IsActive.HasValue)
            user.IsActive = request.IsActive.Value;

        user.LastLogin = DateTime.Now;

        await _context.SaveChangesAsync();

        return Ok(new { message = "User updated successfully", user });
    }

    // ✅ REGISTER
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequests request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            return BadRequest(new { message = "Email already registered" });
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password, // ❗You should hash this in production
            IsActive = true,
            LastLogin = null
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "User registered successfully",
            userId = user.Id
        });
    }
}
