using futshop_dweb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using futshop_dweb.Models;

[Route("api/[controller]")]
[ApiController]
public class APIController : Controller
{
    private readonly ApplicationDbContext _context;

    public APIController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return BadRequest("Invalid client request");
        }

        var user = await _context.Utilizadores
            .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        if (user != null)
        {
            return Ok("ok");
        }
        else
        {
            return Ok("nao existe");
        }
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            return BadRequest("Invalid client request");
        }

        var existingUser = await _context.Utilizadores
            .FirstOrDefaultAsync(u => u.Email == email);

        if (existingUser != null)
        {
            return BadRequest("User already exists");
        }

        var newUser = new Utilizador
        {
            Email = email,
            Password = password
        };

        _context.Utilizadores.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok("User created successfully");
    }
}



