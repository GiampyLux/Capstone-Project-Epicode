using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE_GHREV.Data;
using BE_GHREV.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_GHREV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtentiController : ControllerBase
    {
        private readonly GhrevDB _context;

        public UtentiController(GhrevDB context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utenti>>> GetUtenti()
        {
            return await _context.Utenti.ToListAsync();
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult<Utenti>> GetUtenti(int id)
        {
            var utenti = await _context.Utenti.FindAsync(id);

            if (utenti == null)
            {
                return NotFound();
            }

            return utenti;
        }

        
        [HttpPost]
        public async Task<ActionResult<Utenti>> PostUtenti(Utenti utenti)
        {
            _context.Utenti.Add(utenti);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUtenti), new { id = utenti.ID }, utenti);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtenti(int id, Utenti utenti)
        {
            if (id != utenti.ID)
            {
                return BadRequest();
            }

            _context.Entry(utenti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtentiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtenti(int id)
        {
            var utenti = await _context.Utenti.FindAsync(id);
            if (utenti == null)
            {
                return NotFound();
            }

            _context.Utenti.Remove(utenti);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtentiExists(int id)
        {
            return _context.Utenti.Any(e => e.ID == id);
        }
    }
}
