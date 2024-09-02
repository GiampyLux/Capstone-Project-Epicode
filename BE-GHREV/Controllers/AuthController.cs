using Microsoft.AspNetCore.Mvc;
using BE_GHREV.Data;
using BE_GHREV.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BE_GHREV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GhrevDB _context;

        public AuthController(GhrevDB context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Utenti user)
        {

            if (_context.Utenti.Any(u => u.Email == user.Email))
            {
                return BadRequest("Email already in use.");
            }


            user.Password = PasswordHelper.HashPassword(user.Password);


            _context.Utenti.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login loginModel)
        {
            var user = _context.Utenti.SingleOrDefault(u => u.Email == loginModel.Email);

            if (user == null || !PasswordHelper.VerifyPassword(user.Password, loginModel.Password))
            {
                return Unauthorized("Invalid credentials.");
            }


            return Ok("Login successful.");
        }
    }
}

 
