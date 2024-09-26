using Microsoft.AspNetCore.Mvc;
using BE_GHREV.Data;
using BE_GHREV.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BE_GHREV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly GhrevDB _context;
        private readonly string _jwtSecret = "sS$3cR3tK3y!2Vx8R9#k4g!1sA2bT6zdsasd365bfdsdfdsfds"; // Chiave segreta per la firma del JWT

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

            // Genera il JWT con l'ID utente
            var token = GenerateJwtToken(user);

            return Ok(new { message = "Login successful.", token = token, nome = user.Nome });
        }

        // Metodo per generare il JWT
        private string GenerateJwtToken(Utenti user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret); // Chiave segreta per firmare il token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()), // ID dell'utente
                    new Claim(ClaimTypes.Name, user.Nome) // Nome dell'utente
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Imposta la scadenza del token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); // Restituisci il token come stringa
        }
    }
}
