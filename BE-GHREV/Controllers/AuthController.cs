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

        // Registrazione
        [HttpPost("register")]
        public async Task<IActionResult> Register(Utenti user)
        {
            // Controlla se l'email è già in uso
            if (_context.Utenti.Any(u => u.Email == user.Email))
            {
                return BadRequest(new { message = "Email already in use." });
            }

            // Hash della password prima di salvarla
            user.Password = PasswordHelper.HashPassword(user.Password);

            // Aggiungi l'utente al database
            _context.Utenti.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful." });
        }

        // Login
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login loginModel)
        {
            // Trova l'utente tramite email
            var user = _context.Utenti.SingleOrDefault(u => u.Email == loginModel.Email);

            // Verifica le credenziali (email e password)
            if (user == null || !PasswordHelper.VerifyPassword(user.Password, loginModel.Password))
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            // Se la login è corretta, puoi generare e restituire un token (ad es. JWT)
            // Simuliamo un token di esempio
            var fakeToken = "fake-jwt-token";

            return Ok(new { message = "Login successful.", token = fakeToken, nome = user.Nome }); // Assicurati che `Nome` sia una proprietà di `Utenti`

        }
    }
}
